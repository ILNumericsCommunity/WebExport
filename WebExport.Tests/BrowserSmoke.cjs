// Requires Playwright and its Chromium browser: npm install playwright && npx playwright install chromium
const { chromium } = require('playwright');
const { pathToFileURL } = require('node:url');
const { resolve } = require('node:path');
(async () => {
    if (!process.argv[2]) throw new Error('Pass the offline fixture HTML path.');
    const expected = process.argv.includes('--standard') ? ['scatter', 'scatter', 'scatter', 'scatter', 'mesh3d'] : ['heatmap', 'scatter', 'scatter', 'surface'];
    const browser = await chromium.launch({headless: true,
        ...(process.env.WEBEXPORT_BROWSER_PATH ? {executablePath: process.env.WEBEXPORT_BROWSER_PATH} : {})});
    try {
        const context = await browser.newContext({offline: true});
        const page = await context.newPage();
        const errors = [], requests = [];
        page.on('pageerror', error => errors.push(error.message));
        page.on('request', request => { if (/^https?:/.test(request.url())) requests.push(request.url()); });
        await page.goto(pathToFileURL(resolve(process.argv[2])).href);
        await page.waitForFunction(count => document.querySelectorAll('.js-plotly-plot').length === count, expected.length);
        const types = await page.evaluate(() => Array.from(document.querySelectorAll('.js-plotly-plot')).map(element => {
            if (!element._fullLayout.width || !element._fullLayout.height) throw new Error('Empty chart layout');
            return element._fullData[0].type;
        }));
        if (JSON.stringify(types) !== JSON.stringify(expected) || errors.length || requests.length)
            throw new Error(JSON.stringify({types, errors, requests}));
        if (process.argv.includes('--standard')) {
            const filled = await page.evaluate(() => {
                const chart = document.querySelectorAll('.js-plotly-plot')[2];
                const rect = chart.getBoundingClientRect(), layout = chart._fullLayout;
                const point = new DOMPoint(rect.left + layout.xaxis._offset + layout.xaxis.l2p(2.01),
                    rect.top + layout.yaxis._offset + layout.yaxis.l2p(5.2));
                return Array.from(chart.querySelectorAll('.fills path')).some(path =>
                    getComputedStyle(path).fill === 'rgb(0, 0, 0)' && path.isPointInFill(point.matrixTransform(path.getScreenCTM().inverse())));
            });
            if (!filled) throw new Error('Candlestick body has a hole from overlapping triangle fills.');
        }
        if (process.env.WEBEXPORT_SCREENSHOT_PATH) await page.screenshot({path: process.env.WEBEXPORT_SCREENSHOT_PATH, fullPage: true});
        await page.evaluate(() => {
            window.Plotly.newPlot = () => Promise.reject(new Error('Expected smoke-test failure'));
            const initialization = Array.from(document.scripts).find(script => script.textContent.includes('return Plotly.newPlot'));
            const rerun = document.createElement('script');
            rerun.textContent = initialization.textContent;
            document.body.appendChild(rerun);
        });
        await page.getByText('HTML chart could not be loaded.', {exact: true}).waitFor();
        console.log('PASS: requested trace families render offline; no network requests or JavaScript errors; rendering failure produces a visible message.');
    } finally { await browser.close(); }
})().catch(error => { console.error(error); process.exitCode = 1; });
