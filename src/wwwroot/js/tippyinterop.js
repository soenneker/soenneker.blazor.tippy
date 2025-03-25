export class TippyInterop {
    constructor() {
        this._tippyInstances = {};
        this._observers = {};
    }

    initialize(elementId, options) {
        const element = document.getElementById(elementId);

        if (element) {
            if (this._tippyInstances[elementId]) {
                this._tippyInstances[elementId].destroy();
            }

            template.style.display = 'initial';

            const opt = JSON.parse(options);
            this._tippyInstances[elementId] = tippy(element, opt);

            this.createObserver(elementId);
        }
    }

    show(elementId) {
        if (this._tippyInstances[elementId]) {
            this._tippyInstances[elementId].show();
        }
    }

    hide(elementId) {
        if (this._tippyInstances[elementId]) {
            this._tippyInstances[elementId].hide();
        }
    }

    destroy(elementId) {
        if (this._tippyInstances[elementId]) {
            this._tippyInstances[elementId].destroy();
            delete this._tippyInstances[elementId];
        }

        if (this._observers[elementId]) {
            this._observers[elementId].disconnect();
            delete this._observers[elementId];
        }
    }

    createObserver(elementId) {
        const target = document.getElementById(elementId);
        if (!target || !target.parentNode) return;

        const observer = new MutationObserver((mutations) => {
            const targetRemoved = mutations.some(mutation =>
                Array.from(mutation.removedNodes).includes(target)
            );

            if (targetRemoved) {
                this.destroy(elementId);
            }
        });

        observer.observe(target.parentNode, { childList: true });
        this._observers[elementId] = observer;
    }
}

window.TippyInterop = new TippyInterop();