const {chromium}=require('playwright');
const {pathToFileURL}=require('node:url');
const {resolve,join}=require('node:path');
(async()=>{
    if(!process.argv[2]) throw new Error('Pass WEBEXPORT_PREPARATION_FIXTURES directory.');
    const browser=await chromium.launch({headless:true,...(process.env.WEBEXPORT_BROWSER_PATH ? {executablePath:process.env.WEBEXPORT_BROWSER_PATH}:{})});
    try {
        const context=await browser.newContext({offline:true});
        for(let i=14;i<=22;i++) {
            const page=await context.newPage(),errors=[],requests=[];
            page.on('pageerror',e=>errors.push(e.message));
            page.on('request',r=>{if(/^https?:/.test(r.url())) requests.push(r.url());});
            await page.goto(pathToFileURL(resolve(process.argv[2],`plot-${i}.html`)).href);
            await page.waitForFunction(()=>document.querySelector('.js-plotly-plot')?._fullLayout);
            const stats=await page.evaluate(()=>{
                const plot=document.querySelector('.js-plotly-plot');
                return {traces:plot._fullData.length,vertices:Math.max(...plot.data.map(t=>t.x?.length??0)),
                    types:plot._fullData.map(t=>t.type),range:plot._fullLayout.xaxis?.range,drawn:plot.querySelectorAll('path,canvas').length};
            });
            if(errors.length||requests.length||!stats.traces||!stats.drawn||!stats.vertices) throw new Error(JSON.stringify({i,errors,requests,stats}));
            if([14,21,22].includes(i)&&stats.vertices<=4) throw new Error('Spline was not tessellated');
            if(stats.range?.some(v=>!Number.isFinite(v))) throw new Error('Nonfinite chart bounds');
            if(process.env.WEBEXPORT_PREPARATION_SCREENSHOTS) await page.screenshot({path:join(process.env.WEBEXPORT_PREPARATION_SCREENSHOTS,`plot-${i}.png`),fullPage:true});
            console.log(`PASS plot ${i}: ${stats.traces} traces, ${stats.vertices} vertices`);
            await page.close();
        }
    } finally {await browser.close();}
})().catch(e=>{console.error(e);process.exitCode=1;});
