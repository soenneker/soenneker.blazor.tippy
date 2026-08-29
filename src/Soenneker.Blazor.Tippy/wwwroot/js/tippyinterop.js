const tippyInstances = {};
const observers = {};

export function initialize(elementId, options) {
    const element = document.getElementById(elementId);
    if (!element)
        throw new Error(`Tippy target '${elementId}' was not found.`);

    destroy(elementId);

    const parsedOptions = typeof options === "string" ? JSON.parse(options) : (options ?? {});
    const { useCdn, ...tippyOptions } = parsedOptions;

    tippyInstances[elementId] = globalThis.tippy(element, tippyOptions);
    createObserver(elementId, element);
}

export function show(elementId) {
    requireInstance(elementId).show();
}

export function hide(elementId) {
    requireInstance(elementId).hide();
}

export function destroy(elementId) {
    const instance = tippyInstances[elementId];
    if (instance) {
        instance.destroy();
        delete tippyInstances[elementId];
    }

    const observer = observers[elementId];
    if (observer) {
        observer.disconnect();
        delete observers[elementId];
    }
}

export function dispose() {
    for (const elementId of Object.keys(tippyInstances))
        destroy(elementId);

    for (const elementId of Object.keys(observers)) {
        observers[elementId].disconnect();
        delete observers[elementId];
    }
}

function createObserver(elementId, target) {
    const observer = new MutationObserver(() => {
        if (!target.isConnected)
            destroy(elementId);
    });

    const observationRoot = document.body ?? document.documentElement;
    if (observationRoot) {
        observer.observe(observationRoot, { childList: true, subtree: true });
        observers[elementId] = observer;
    }
}

function requireInstance(elementId) {
    const instance = tippyInstances[elementId];
    if (!instance)
        throw new Error(`Tippy instance '${elementId}' was not found.`);

    return instance;
}
