let voiceHandler = null;

API.onResourceStart.connect(() => { 
    voiceHandler = new GtmpVoiceHandler(); 
});

API.onServerEventTrigger.connect((eventName, args) => {
    switch(eventName) {
        case "VOICE_SET_HANDSHAKE": {
            if(args.Length === 2) {
                voiceHandler.setHandshake(args[0], args[1]);
            } else {
                voiceHandler.setHandshake(args[0], "");
            }            
            break;
        }
    }
});

class GtmpVoiceHandler {
    
    constructor() {        
        this.handshakeTimer = -1;   
        
        this.buildBrowser();        
    }
    
    buildBrowser() {
        this.browser = API.createCefBrowser(0, 0, false);
        API.waitUntilCefBrowserInit(this.browser);
        API.setCefBrowserHeadless(this.browser, true);
    }
    
    sendHandshake(url) {
        API.sendChatMessage("HS: " + url);
        API.loadPageCefBrowser(this.browser, url);
    }
    
    setHandshake(status, url) {
        if (status) {
            this.handshakeTimer = API.every(1000, "resendHandshake", url);
            resendHandshake(url);
        } else {
            API.stop(this.handshakeTimer);
            this.handshakeTimer = -1;
        }
    }
    
}

function resendHandshake(url) {
    voiceHandler.sendHandshake(url);
}
