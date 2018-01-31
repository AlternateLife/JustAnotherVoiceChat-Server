/*
 * File: GtmpVoice.js
 * Date: 29.1.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

let voiceHandler = null;
let rotationThreshold = 2;

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
        const rotation = API.getGameplayCamDir().Z;

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
