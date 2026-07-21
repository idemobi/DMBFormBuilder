(function () {
    const formInitialStates = new WeakMap();
    const initializedSubmitForms = new WeakSet();

    function findFieldByName(form, name) {
        if (!form || !name) {
            return null;
        }

        return Array.prototype.find.call(form.elements, function (element) {
            return element.name === name;
        }) || null;
    }

    function getValidationMessage(input) {
        let message = input.validationMessage;

        if (input.validity.valueMissing) {
            message = input.getAttribute("data-val-required") || message;
        } else if (input.validity.typeMismatch) {
            message = input.getAttribute("data-val-email") || message;
        } else if (input.validity.tooShort) {
            message = input.getAttribute("data-val-minlength") || message;
        } else if (input.validity.tooLong) {
            message = input.getAttribute("data-val-maxlength") || message;
        } else if (input.validity.patternMismatch) {
            message = input.getAttribute("data-val-regex") || message;
        } else if (input.validity.rangeUnderflow || input.validity.rangeOverflow) {
            message = input.getAttribute("data-val-range") || message;
        }

        return message;
    }

    function isEqualToValid(input) {
        const otherName = input.getAttribute("data-val-equalto-other");
        if (!otherName || input.value.length === 0) {
            return true;
        }

        const otherInput = findFieldByName(input.form, otherName);
        return !otherInput || input.value === otherInput.value;
    }

    function isNoHtmlValid(input) {
        if (!input.getAttribute("data-val-nohtml") || input.value.length === 0) {
            return true;
        }

        return !/[<>]/.test(input.value);
    }

    function isBooleanConstraintValid(input) {
        const expected = input.getAttribute("data-val-bool-expected");
        if (!expected) {
            return true;
        }

        return input.checked === (expected === "true");
    }

    function isFlagRequiredValid(input) {
        if (!input.getAttribute("data-val-flag-required")) {
            return true;
        }

        return (parseInt(input.value, 10) || 0) !== 0;
    }

    function isFieldValidationCandidate(input) {
        return input
            && !input.disabled
            && !input.getAttribute("data-formbuilder-ignore-validation")
            && !input.closest("fieldset[disabled]");
    }

    function getFieldValidationState(input) {
        if (!input) {
            return {
                isValid: true,
                message: ""
            };
        }

        if (!isFieldValidationCandidate(input)) {
            return {
                isValid: true,
                message: ""
            };
        }

        const isEqualTo = isEqualToValid(input);
        const isNoHtml = isNoHtmlValid(input);
        const isBooleanConstraint = isBooleanConstraintValid(input);
        const isFlagRequired = isFlagRequiredValid(input);
        const isValid = input.checkValidity() && isEqualTo && isNoHtml && isBooleanConstraint && isFlagRequired;
        const message = !isEqualTo
            ? input.getAttribute("data-val-equalto") || getValidationMessage(input)
            : !isNoHtml
                ? input.getAttribute("data-val-nohtml") || getValidationMessage(input)
                : !isBooleanConstraint
                    ? input.getAttribute("data-val-bool") || getValidationMessage(input)
                    : !isFlagRequired
                        ? input.getAttribute("data-val-flag-required") || getValidationMessage(input)
                        : getValidationMessage(input);

        return {
            isValid: isValid,
            message: message
        };
    }

    function updateFieldState(input) {
        const state = getFieldValidationState(input);

        if (!isFieldValidationCandidate(input)) {
            return true;
        }

        const field = input.closest(".dmb-form-field");
        const feedback = field ? field.querySelector(".invalid-feedback") : null;

        if (!state.isValid && feedback) {
            feedback.textContent = state.message;
        }

        input.classList.toggle("is-invalid", !state.isValid);
        input.classList.toggle("is-valid", state.isValid && input.value.trim().length > 0);

        if (feedback) {
            feedback.classList.toggle("d-block", !state.isValid);
        }

        return state.isValid;
    }

    function getFormFields(form) {
        if (!form) {
            return [];
        }

        return Array.prototype.slice.call(form.querySelectorAll(".dmb-form-field input, .dmb-form-field textarea, .dmb-form-field select"));
    }

    function isFieldChangeCandidate(input) {
        return input
            && !input.disabled
            && input.name !== "__RequestVerificationToken"
            && input.type !== "button"
            && input.type !== "submit"
            && input.type !== "reset"
            && !input.getAttribute("data-formbuilder-ignore-change")
            && !input.closest("fieldset[disabled]");
    }

    function getFormChangeFields(form) {
        if (!form) {
            return [];
        }

        return Array.prototype.slice.call(form.elements).filter(function (element) {
            return element && /^(INPUT|TEXTAREA|SELECT)$/i.test(element.tagName || "");
        });
    }

    function getFieldChangeValue(input) {
        if (!input) {
            return "";
        }

        if (input.type === "checkbox" || input.type === "radio") {
            return {
                checked: input.checked,
                value: input.value || ""
            };
        }

        if (input.tagName === "SELECT" && input.multiple) {
            return Array.prototype.slice.call(input.selectedOptions).map(function (option) {
                return option.value;
            });
        }

        if (input.type === "file") {
            return Array.prototype.slice.call(input.files || []).map(function (file) {
                return {
                    name: file.name,
                    size: file.size,
                    lastModified: file.lastModified
                };
            });
        }

        return input.value || "";
    }

    function getFormChangeState(form) {
        return JSON.stringify(getFormChangeFields(form)
            .filter(isFieldChangeCandidate)
            .map(function (field, index) {
                return {
                    index: index,
                    name: field.name || "",
                    id: field.id || "",
                    type: field.type || field.tagName || "",
                    value: getFieldChangeValue(field)
                };
            }));
    }

    function ensureInitialFormState(form) {
        if (!form || formInitialStates.has(form)) {
            return;
        }

        formInitialStates.set(form, getFormChangeState(form));
    }

    function isFormChanged(form) {
        if (!form) {
            return false;
        }

        ensureInitialFormState(form);
        return formInitialStates.get(form) !== getFormChangeState(form);
    }

    function isFormCurrentlyValid(form) {
        return getFormFields(form).every(function (field) {
            return getFieldValidationState(field).isValid;
        });
    }

    function isSubmitLockEnabled(form) {
        return form && form.getAttribute("data-submit-when-valid") === "true";
    }

    function isSubmitChangeLockEnabled(form) {
        return form && form.getAttribute("data-submit-when-changed") === "true";
    }

    function updateSubmitButtons(form) {
        const submitWhenValid = isSubmitLockEnabled(form);
        const submitWhenChanged = isSubmitChangeLockEnabled(form);
        if (!submitWhenValid && !submitWhenChanged) {
            return;
        }

        const isValid = !submitWhenValid || isFormCurrentlyValid(form);
        const isChanged = !submitWhenChanged || isFormChanged(form);
        const canSubmit = isValid && isChanged;
        const buttons = form.querySelectorAll("[data-formbuilder-submit-lock='true'], button[type='submit'], button:not([type]), input[type='submit']");
        const resetButtons = submitWhenChanged
            ? form.querySelectorAll("[data-formbuilder-reset='true'], [data-formbuilder-reset-lock='true']")
            : [];

        buttons.forEach(function (button) {
            if (button.hasAttribute("formnovalidate") || button.getAttribute("data-formbuilder-submit-lock-ignore") === "true") {
                return;
            }

            button.disabled = !canSubmit;
            button.classList.toggle("disabled", !canSubmit);
            button.setAttribute("aria-disabled", canSubmit ? "false" : "true");
        });

        resetButtons.forEach(function (button) {
            button.disabled = !isChanged;
            button.classList.toggle("disabled", !isChanged);
            button.setAttribute("aria-disabled", isChanged ? "false" : "true");
        });
    }

    function initializeSubmitForm(form) {
        if (!form || (!isSubmitLockEnabled(form) && !isSubmitChangeLockEnabled(form))) {
            return;
        }

        ensureInitialFormState(form);

        if (!initializedSubmitForms.has(form)) {
            form.addEventListener("input", function () {
                updateSubmitButtons(form);
            });
            form.addEventListener("change", function () {
                updateSubmitButtons(form);
            });
            form.addEventListener("reset", function () {
                window.setTimeout(function () {
                    updateSubmitButtons(form);
                }, 0);
            });
            initializedSubmitForms.add(form);
        }

        updateSubmitButtons(form);
    }

    function refreshForm(form) {
        initializeDescriptionPopovers(form);
        initializeSubmitForm(form);
    }

    function initializeDescriptionPopovers(root) {
        if (!window.bootstrap || !window.bootstrap.Popover) {
            return;
        }

        const scope = root && root.querySelectorAll ? root : document;
        scope.querySelectorAll("[data-dmb-form-field-description][data-bs-toggle='popover']").forEach(function (target) {
            if (target.getAttribute("data-dmb-popover-bound") === "true") {
                return;
            }

            new window.bootstrap.Popover(target, {
                container: "body",
                trigger: target.getAttribute("data-bs-trigger") || "hover focus"
            });
            target.setAttribute("data-dmb-popover-bound", "true");
        });
    }

    function cleanInputValue(input) {
        const cleaner = input.getAttribute("data-formbuilder-cleaner");
        if (!cleaner || input.value.length === 0) {
            return;
        }

        const original = input.value;
        if (cleaner === "ascii") {
            input.value = original.replace(/[^\x20-\x7E]/g, "");
        } else if (cleaner === "unix") {
            input.value = original.replace(/[^a-zA-Z0-9_]/g, "");
        }
    }

    function updateFieldCounters(input) {
        if (!input || !input.id) {
            return;
        }

        document.querySelectorAll('[data-dmb-counter-for="' + input.id + '"]').forEach(function (badge) {
            const max = badge.getAttribute("data-dmb-counter-max");
            badge.textContent = input.value.length + (max ? " / " + max : "");
        });
    }

    function updatePasswordStrength(input) {
        if (!input) {
            return;
        }

        const container = document.querySelector('[data-dmb-password-strength="' + input.id + '"]');
        const progressBar = container ? container.querySelector(".progress-bar") : null;
        if (!container || !progressBar) {
            return;
        }

        const password = input.value || "";
        let strength = 100;

        if (password.length < 18) {
            strength -= (18 - password.length) * (100 / 18);
        }

        if (!/[A-Z]/.test(password) || !/[a-z]/.test(password)) {
            strength /= 2;
        }

        if (!/[0-9]/.test(password)) {
            strength /= 1.5;
        }

        if (!/[^a-zA-Z0-9\s]/.test(password)) {
            strength /= 1.3;
        }

        strength = Math.max(5, Math.min(strength, 100));

        progressBar.style.width = strength + "%";
        progressBar.classList.toggle("bg-danger", strength < 50);
        progressBar.classList.toggle("bg-warning", strength >= 50 && strength < 75);
        progressBar.classList.toggle("bg-success", strength >= 75);
        container.setAttribute("aria-valuenow", Math.round(strength).toString());
    }

    function updateFlagField(hiddenId) {
        const hidden = document.getElementById(hiddenId);
        if (!hidden) {
            return;
        }

        let value = 0;
        document.querySelectorAll('[data-dmb-flag-for="' + hiddenId + '"]').forEach(function (control) {
            if (control.tagName === "SELECT") {
                Array.prototype.forEach.call(control.selectedOptions, function (option) {
                    value |= parseInt(option.value, 10) || 0;
                });
            } else if (control.checked) {
                value |= parseInt(control.value, 10) || 0;
            }
        });

        hidden.value = value.toString();
        updateFieldState(hidden);
    }

    function syncSliderTextField(source, target) {
        if (!source || !target || target.value === source.value) {
            return;
        }

        target.value = source.value;
        updateFieldState(target);
    }

    function syncSliderCompanion(input) {
        if (!input) {
            return;
        }

        if (input.getAttribute("data-dmb-slider") === "true") {
            syncSliderTextField(input, document.querySelector('[data-dmb-slider-text-for="' + input.id + '"]'));
            return;
        }

        const sliderId = input.getAttribute("data-dmb-slider-text-for");
        if (sliderId) {
            syncSliderTextField(input, document.getElementById(sliderId));
        }
    }

    document.addEventListener("click", function (event) {
        const passwordButton = event.target.closest("[data-dmb-password-toggle]");
        if (passwordButton) {
            const input = document.getElementById(passwordButton.getAttribute("data-dmb-password-toggle"));
            const icon = passwordButton.querySelector(".bi");
            const canToggleReadonly = passwordButton.getAttribute("data-dmb-password-toggle-readonly") === "true";
            if (!input || input.disabled || (input.readOnly && !canToggleReadonly)) {
                return;
            }

            const isHidden = input.type === "password";
            input.type = isHidden ? "text" : "password";

            if (icon) {
                icon.classList.toggle("bi-eye", !isHidden);
                icon.classList.toggle("bi-eye-slash", isHidden);
            }

            passwordButton.setAttribute("aria-label", isHidden
                ? passwordButton.getAttribute("data-dmb-password-hide-label") || ""
                : passwordButton.getAttribute("data-dmb-password-show-label") || "");
        }

        const tokenToggle = event.target.closest("[data-dmb-token-toggle]");
        if (tokenToggle) {
            const input = document.getElementById(tokenToggle.getAttribute("data-dmb-token-toggle"));
            const icon = tokenToggle.querySelector(".bi");
            if (!input) {
                return;
            }

            const isHidden = input.type === "password";
            input.type = isHidden ? "text" : "password";

            if (icon) {
                icon.classList.toggle("bi-eye", !isHidden);
                icon.classList.toggle("bi-eye-slash", isHidden);
            }

            tokenToggle.setAttribute("aria-label", isHidden
                ? tokenToggle.getAttribute("data-dmb-token-hide-label") || ""
                : tokenToggle.getAttribute("data-dmb-token-show-label") || "");
        }

        const tokenCopy = event.target.closest("[data-dmb-token-copy]");
        if (tokenCopy) {
            const input = document.getElementById(tokenCopy.getAttribute("data-dmb-token-copy"));
            const icon = tokenCopy.querySelector(".bi");
            if (!input) {
                return;
            }

            const copy = navigator.clipboard && navigator.clipboard.writeText
                ? navigator.clipboard.writeText(input.value)
                : Promise.reject();

            copy.then(function () {
                if (!icon) {
                    return;
                }

                icon.classList.remove("bi-clipboard");
                icon.classList.add("bi-clipboard-check");
                window.setTimeout(function () {
                    icon.classList.remove("bi-clipboard-check");
                    icon.classList.add("bi-clipboard");
                }, 1500);
            }).catch(function () {
                const wasDisabled = input.disabled;
                const wasReadOnly = input.readOnly;
                input.disabled = false;
                input.readOnly = true;
                input.select();
                document.execCommand("copy");
                input.readOnly = wasReadOnly;
                input.disabled = wasDisabled;
            });
        }

        const selectValueButton = event.target.closest("[data-dmb-select-value-for]");
        if (selectValueButton) {
            const select = document.getElementById(selectValueButton.getAttribute("data-dmb-select-value-for"));
            if (!select) {
                return;
            }

            select.value = selectValueButton.getAttribute("data-dmb-select-value") || "";
            select.dispatchEvent(new Event("change", { bubbles: true }));
        }
    });

    document.addEventListener("input", function (event) {
        const input = event.target && event.target.closest
            ? event.target.closest(".dmb-form-field input, .dmb-form-field textarea, .dmb-form-field select")
            : null;
        const changedForm = event.target && event.target.closest
            ? event.target.closest("form.dmb-form-builder[data-submit-when-changed='true']")
            : null;

        if (input) {
            cleanInputValue(input);
            syncSliderCompanion(input);
            updateFieldState(input);
            updateSubmitButtons(input.form);
            updateFieldCounters(input);
            updatePasswordStrength(input);

            const form = input.form;
            if (form && input.name) {
                form.querySelectorAll('[data-val-equalto-other="' + input.name + '"]').forEach(updateFieldState);
            }
        }

        if (changedForm) {
            updateSubmitButtons(changedForm);
        }

        const flagControl = event.target && event.target.closest
            ? event.target.closest("[data-dmb-flag-for]")
            : null;
        if (flagControl) {
            updateFlagField(flagControl.getAttribute("data-dmb-flag-for"));
        }
    });

    document.addEventListener("change", function (event) {
        const input = event.target && event.target.closest
            ? event.target.closest(".dmb-form-field input, .dmb-form-field textarea, .dmb-form-field select")
            : null;
        const changedForm = event.target && event.target.closest
            ? event.target.closest("form.dmb-form-builder[data-submit-when-changed='true']")
            : null;

        if (input) {
            syncSliderCompanion(input);
            updateFieldState(input);
            updateSubmitButtons(input.form);
        }

        if (changedForm) {
            updateSubmitButtons(changedForm);
        }

        const flagControl = event.target && event.target.closest
            ? event.target.closest("[data-dmb-flag-for]")
            : null;
        if (flagControl) {
            updateFlagField(flagControl.getAttribute("data-dmb-flag-for"));
        }
    });

    document.addEventListener("submit", function (event) {
        const form = event.target;

        if (!form || !form.matches(".dmb-form-builder")) {
            return;
        }

        const fields = form.querySelectorAll(".dmb-form-field input, .dmb-form-field textarea, .dmb-form-field select");
        let isValid = true;

        fields.forEach(function (field) {
            if (!updateFieldState(field)) {
                isValid = false;
            }
        });

        if (!isValid) {
            event.preventDefault();
            event.stopPropagation();
        }

        form.classList.add("was-validated");
        updateSubmitButtons(form);
    }, true);

    document.addEventListener("reset", function (event) {
        const form = event.target;
        if (!form || !form.matches(".dmb-form-builder")) {
            return;
        }

        window.setTimeout(function () {
            updateSubmitButtons(form);
        }, 0);
    }, true);

    document.addEventListener("dmb-formbuilder-refresh", function (event) {
        const form = event.target && event.target.matches && event.target.matches("form")
            ? event.target
            : event.target && event.target.closest
                ? event.target.closest("form")
                : null;

        if (form) {
            refreshForm(form);
        }
    });

    function initializePage() {
        initializeDescriptionPopovers(document);
        document.querySelectorAll("[data-dmb-password-input]").forEach(updatePasswordStrength);
        document.querySelectorAll("[data-dmb-flag-hidden]").forEach(function (hidden) {
            updateFlagField(hidden.id);
        });
        document.querySelectorAll("[data-dmb-counter-for]").forEach(function (badge) {
            const input = document.getElementById(badge.getAttribute("data-dmb-counter-for"));
            updateFieldCounters(input);
        });
        document.querySelectorAll("form.dmb-form-builder[data-submit-when-valid='true'], form.dmb-form-builder[data-submit-when-changed='true']").forEach(initializeSubmitForm);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initializePage);
    } else {
        initializePage();
    }

    window.DMBFormBuilder = window.DMBFormBuilder || {};
    window.DMBFormBuilder.refreshForm = refreshForm;
    window.DMBFormBuilder.initializeDescriptionPopovers = initializeDescriptionPopovers;
    window.DMBFormBuilder.updateSubmitButtons = updateSubmitButtons;
    window.DMBFormBuilder.resetInitialState = function (form) {
        if (!form) {
            return;
        }

        formInitialStates.set(form, getFormChangeState(form));
        updateSubmitButtons(form);
    };
})();
