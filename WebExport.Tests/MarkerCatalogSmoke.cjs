const {chromium}=require('playwright');
const {pathToFileURL}=require('node:url');
const {join}=require('node:path');
(async()=>{
 const browser=await chromium.launch({headless:true,executablePath:process.env.WEBEXPORT_BROWSER_PATH});
 try {
  const page=await browser.newPage(); const errors=[];
  page.on('pageerror',e=>errors.push(e.message));
  for(const index of [23,24,25]) {
   await page.goto(pathToFileURL(join(process.argv[2],`plot-${index}.html`)).href);
   await page.waitForFunction(()=>document.querySelector('.js-plotly-plot')?._fullData?.length>0);
   const result=await page.evaluate(()=>{
    const p=document.querySelector('.js-plotly-plot');
    return {count:p._fullData.length,validation:Plotly.validate(p.data,p.layout),paths:p.querySelectorAll('.scatterlayer .point').length};
   });
   if(result.count!==(index===25?4:12)||result.validation?.some(v=>v.code!=='unused')) throw new Error(JSON.stringify({index,result}));
   console.log(JSON.stringify({index,...result}));
  }
  if(errors.length)throw new Error(JSON.stringify(errors));
  console.log('PASS: all shared marker scenes load with valid Plotly data and no browser errors.');
 }finally{await browser.close();}
})().catch(e=>{console.error(e);process.exitCode=1;});

