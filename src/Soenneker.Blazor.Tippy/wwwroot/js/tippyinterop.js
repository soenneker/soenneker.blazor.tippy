const tippyInstances = {};
const observers = {};

export function initialize(elementId, options) {
    const element = document.getElementById(elementId);

    if (element) {
        if (tippyInstances[elementId]) {
            tippyInstances[elementId].destroy();
        }

        template.style.display = 'initial';

        const opt = JSON.parse(options);
        tippyInstances[elementId] = tippy(element, opt);

        createObserver(elementId);
    }
}

export function show(elementId) {
    if (tippyInstances[elementId]) {
        tippyInstances[elementId].show();
    }
}

export function hide(elementId) {
    if (tippyInstances[elementId]) {
        tippyInstances[elementId].hide();
    }
}

export function destroy(elementId) {
    if (tippyInstances[elementId]) {
        tippyInstances[elementId].destroy();
        delete tippyInstances[elementId];
    }

    if (observers[elementId]) {
        observers[elementId].disconnect();
        delete observers[elementId];
    }
}

function createObserver(elementId) {
    const target = document.getElementById(elementId);
    if (!target || !target.parentNode) return;

    const observer = new MutationObserver((mutations) => {
        const targetRemoved = mutations.some(mutation =>
            Array.from(mutation.removedNodes).includes(target)
        );

        if (targetRemoved) {
            destroy(elementId);
        }
    });

    observer.observe(target.parentNode, { childList: true });
    observers[elementId] = observer;
}
