let voiceHandler = null;
let rotationThreshold = 0.01;

API.onResourceStart.connect(() => { voiceHandler = new GtmpVoiceHandler(); });

API.onResourceStop.connect(() => { 
    voiceHandler.dispose(); 
    voiceHandler = null;
});

API.onServerEventTrigger.connect((eventName, args) => {
    switch (eventName) {
        case "VOICE_SET_HANDSHAKE": {
            if (args.Length === 2) {
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
        this.rotationTimer = -1;
        this.lastRotation = 0;

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

    sendRotation() {
        const gameplayCamRotation = API.getGameplayCamRot();
        const rotation = Math.PI / 180 * (gameplayCamRotation.Z * -1);

        if (Math.abs(this.lastRotation - rotation) < rotationThreshold) {
            return;
        }

        API.sendChatMessage("Rotation: " + rotation);
        API.triggerServerEvent("VOICE_ROTATION", rotation);

        this.lastRotation = rotation;
    }

    setHandshake(status, url) {
        if (status === (this.handshakeTimer !== -1)) {
            return;
        }
        
        if (status) {
            if (this.rotationTimer !== -1) {
                API.stop(this.rotationTimer);
                this.rotationTimer = -1;
            }

            this.handshakeTimer = API.every(1000, "resendHandshake", url);
            resendHandshake(url);
        } else {
            if (this.handshakeTimer !== -1) {
                API.stop(this.handshakeTimer);
                this.handshakeTimer = -1;
            }

            this.rotationTimer = API.every(333, "sendCamrotation");
        }
    }

    dispose() {
        if (this.handshakeTimer !== -1) {
            API.stop(this.handshakeTimer);
            this.handshakeTimer = -1;
        }

        if (this.rotationTimer !== -1) {
            API.stop(this.rotationTimer);
            this.rotationTimer = -1;
        }
    }

}

function resendHandshake(url) {
    if (voiceHandler === null) {
        return;
    }
    
    voiceHandler.sendHandshake(url);
}

function sendCamrotation() {
    if (voiceHandler === null) {
        return;
    }
    
    voiceHandler.sendRotation();
}
