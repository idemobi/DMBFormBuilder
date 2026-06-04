(function () {
    function hashCaptchaValue(value) {
        if (!window.CryptoJS || !window.CryptoJS.SHA256) {
            return null;
        }

        return window.CryptoJS.SHA256((value || "").trim()).toString();
    }

    function findFeedback(input) {
        const inputGroup = input.closest(".input-group");
        if (inputGroup) {
            const feedback = inputGroup.querySelector(".invalid-feedback");
            if (feedback) {
                return feedback;
            }
        }

        const container = input.closest("[data-dmb-captcha='true']");
        return container ? container.querySelector(".invalid-feedback") : null;
    }

    function setInputValidity(input, valid, message) {
        const feedback = findFeedback(input);
        const validationMessage = valid ? "" : message || "";

        if (typeof input.setCustomValidity === "function") {
            input.setCustomValidity(validationMessage);
        }

        if (feedback && message) {
            feedback.textContent = message;
        }

        input.classList.toggle("is-valid", valid);
        input.classList.toggle("is-invalid", !valid);
    }

    function validateCaptchaInput(input) {
        const value = input.value || "";
        const expectedHash = input.getAttribute("data-hash") || "";
        const invalidMessage = input.getAttribute("data-val-captchahash") || "";
        const requiredMessage = input.getAttribute("data-val-required") || invalidMessage;

        if (input.hasAttribute("required") && !value.trim()) {
            setInputValidity(input, false, requiredMessage);
            return false;
        }

        if (!value.trim()) {
            setInputValidity(input, true, "");
            return true;
        }

        const actualHash = hashCaptchaValue(value);
        if (actualHash === null) {
            return true;
        }

        const isValid = actualHash === expectedHash;
        setInputValidity(input, isValid, invalidMessage);
        return isValid;
    }

    function refreshFormState(input) {
        if (!input || !input.form) {
            return;
        }

        if (window.DMBFormBuilder && typeof window.DMBFormBuilder.refreshForm === "function") {
            window.DMBFormBuilder.refreshForm(input.form);
            return;
        }

        input.form.dispatchEvent(new CustomEvent("dmb-formbuilder-refresh", { bubbles: true }));
    }

    function registerJQueryValidation() {
        if (!window.jQuery || !window.jQuery.validator) {
            return;
        }

        if (!window.jQuery.validator.methods.CaptchaHash) {
            window.jQuery.validator.addMethod("CaptchaHash", function (value, element) {
                const expectedHash = window.jQuery(element).data("hash");
                const actualHash = hashCaptchaValue(value);
                return actualHash === null || actualHash === expectedHash;
            });
        }

        if (window.jQuery.validator.unobtrusive && window.jQuery.validator.unobtrusive.adapters) {
            window.jQuery.validator.unobtrusive.adapters.add("CaptchaHash", [], function (options) {
                options.rules.CaptchaHash = true;
                if (options.message) {
                    options.messages.CaptchaHash = options.message;
                }
            });
        }
    }

    function refreshCaptcha(container) {
        const input = container.querySelector("input[data-val-captchahash]");
        const image = container.querySelector("img[id$='-img-captcha']");
        const refreshUrl = container.getAttribute("data-dmb-captcha-refresh-url") || "/Captcha/RefreshCaptcha";

        if (!input || !image) {
            return;
        }

        fetch(refreshUrl)
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                image.src = "data:image/png;base64," + data.image;
                input.value = "";
                input.classList.add("is-invalid");
                input.classList.remove("is-valid");
                input.setAttribute("data-hash", data.hash || "");
                input.setAttribute("data-val-captchahash", data.message || "");
                setInputValidity(input, false, data.message || "");
                refreshFormState(input);

                if (window.jQuery && window.jQuery.fn && window.jQuery.fn.validate) {
                    const $input = window.jQuery(input);
                    const validator = $input.closest("form").validate();
                    $input.data("hash", data.hash || "");

                    if (validator) {
                        validator.resetForm();
                        validator.element(input);
                    }
                }
            });
    }

    document.addEventListener("click", function (event) {
        const button = event.target.closest("[data-dmb-captcha-refresh]");

        if (!button) {
            return;
        }

        const container = button.closest("[data-dmb-captcha='true']");

        if (container) {
            refreshCaptcha(container);
        }
    });

    document.addEventListener("input", function (event) {
        const input = event.target;

        if (!input || !input.matches || !input.matches("input[data-val-captchahash]")) {
            return;
        }

        validateCaptchaInput(input);
        refreshFormState(input);
    });

    document.addEventListener("change", function (event) {
        const input = event.target;

        if (!input || !input.matches || !input.matches("input[data-val-captchahash]")) {
            return;
        }

        validateCaptchaInput(input);
        refreshFormState(input);
    });

    document.addEventListener("submit", function (event) {
        const form = event.target;

        if (!form || !form.querySelectorAll) {
            return;
        }

        const captchaInputs = form.querySelectorAll("[data-dmb-captcha='true'] input[required]");
        let isValid = true;

        captchaInputs.forEach(function (input) {
            if (!validateCaptchaInput(input)) {
                isValid = false;
            }
        });

        if (!isValid) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add("was-validated");
        }
    }, true);

    registerJQueryValidation();
})();
