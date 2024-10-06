export class TippyInterop {
    constructor() {
        this._tippyInstances = {};
    }

    init(elementId, options) {
        var element = document.getElementById(elementId);

        if (element) {
            template.style.display = 'initial';

            const opt = JSON.parse(options);
            this._tippyInstances[elementId] = tippy(element, opt);
        }
    }

    hide(elementId) {
        if (this._tippyInstances[elementId]) {
            this._tippyInstances[elementId].hide();
        }
    }
}

window.TippyInterop = new TippyInterop();