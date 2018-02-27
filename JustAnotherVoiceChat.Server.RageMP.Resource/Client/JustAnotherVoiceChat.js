// COOL

let voiceScript = null;

class VoiceScript {

    constructor() {
        this.handshakeUrl = "";

        this.browser = null;
        this.handshakeTimer = -1;
    }

    startHandshake(url) {
        this.handshakeUrl = url;

        if(this.browser == null) {
            this.browser = mp.browsers.new(url);
        }

        if (this.handshakeTimer === -1) {
            this.handshakeTimer = setInterval(this.refreshHandshake.bind(this), 3000);
            this.refreshHandshake();
        }
    }

    stopHandshake() {
        if(this.handshakeTimer !== -1) {
            clearInterval(this.handshakeTimer);
            this.handshakeTimer = -1;
        }
    }

    refreshHandshake() {
        mp.gui.chat.push("HS: " + this.handshakeUrl);
        this.browser.url = this.handshakeUrl;
    }

    dispose() {
        if(this.browser !== null) {
            this.browser.destroy();
            this.browser = null;
        }
    }

}

mp.events.add({
    "guiReady": () => {
        mp.gui.chat.push("Start HUD");
        voiceScript = new VoiceScript();
    },
    "voiceSetHandhsake": (newStatus, handshakeUrl) => {
        if(newStatus) {
            voiceScript.startHandshake(handshakeUrl);
        } else {
            voiceScript.stopHandshake();
        }
    },
    "playerQuit": (player, exittype, reason) => {
        if(voiceScript !== null) {
            voiceScript.dispose();
            voiceScript = null;
        }
    }
});
