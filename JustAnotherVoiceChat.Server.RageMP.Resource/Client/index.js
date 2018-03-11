let voiceScript = null;

class VoiceScript {

    constructor() {
        this.handshakeUrl = "";

        this.browser = null;
        
        this.handshakeTimer = -1;
        this.rotationTimer = -1;
        
        this.rotationThreshold = 0.05;
        this.oldRotation = 0;
    }

    startHandshake(url) {
        this.handshakeUrl = url;

        if(this.browser == null) {
            this.browser = mp.browsers.new(url);
            this.browser.active = false;
        }
        
        if (this.rotationTimer !== -1) {
            clearInterval(this.rotationTimer);
            this.rotationTimer = -1;
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
        
        if (this.rotationTimer === -1) {
            this.rotationTimer = setInterval(this.updateRotation.bind(this), 333);
            this.updateRotation();
        }        
    }

    refreshHandshake() {
        this.browser.url = this.handshakeUrl;
    }

    updateRotation() {
        const rotation = ((mp.game.cam.getGameplayCamRot(0).z * -1) * Math.PI) / 180;
        
        if (Math.abs(this.oldRotation - rotation) < this.rotationThreshold) {
            return;
        }
        
        mp.gui.chat.push("ROTATION: " + mp.game.cam.getGameplayCamRot(0).z + " -> " + rotation);
        
        this.oldRotation = rotation;
        mp.events.callRemote("updateRotation", rotation);
    }

    dispose() {
        if(this.browser !== null) {
            this.browser.destroy();
            this.browser = null;
        }
    }

}

mp.events.add({
    "voiceSetHandhsake": (newStatus, handshakeUrl) => {
        if (voiceScript == null) {
            voiceScript = new VoiceScript();            
        }
        
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
