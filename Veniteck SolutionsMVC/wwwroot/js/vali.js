// Email validation function
function validateEmail(email) {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
}

// Mobile number validation function (10-digit validation)
function validateMobileNumber(mobileNumber) {
    const mobilePattern = /^[0-9]{10}$/; // Validates a 10-digit number
    return mobilePattern.test(mobileNumber);
}

// Form validation on submit
$(document).ready(function () {
    $('#userForm').submit(function (event) {
        let valid = true;

        // Reset error messages and input field styling
        $('.error-message').hide();
        $('.form-control').removeClass('invalid-input'); // Remove red border

        // Name validation
        const name = $('#name').val();
        if (!name) {
            valid = false;
            $('#name').addClass('invalid-input');
            $('#nameError').show();
        }

        // Address validation
        const address = $('#address').val();
        if (!address) {
            valid = false;
            $('#address').addClass('invalid-input');
            $('#addressError').show();
        }

        // Email validation
        const email = $('#email').val();
        if (!validateEmail(email)) {
            valid = false;
            $('#email').addClass('invalid-input');
            $('#emailError').show();
        }

        // Mobile number validation
        const mobileNumber = $('#mobileNumber').val();
        if (!validateMobileNumber(mobileNumber)) {
            valid = false;
            $('#mobileNumber').addClass('invalid-input');
            $('#mobileNumberError').show();
        }

        // Source validation
        const source = $('#source').val();
        if (!source) {
            valid = false;
            $('#source').addClass('invalid-input');
            $('#sourceError').show();
        }

        // If validation fails, prevent form submission
        if (!valid) {
            event.preventDefault(); // Prevent form from submitting
        }
    });

    // Reset error message and input border when user starts typing in the input fields
    $('.form-control').on('input', function () {
        const input = $(this);
        const errorMessage = $("#" + input.attr('id') + "Error");

        // Remove red border and hide error message when user starts typing
        input.removeClass('invalid-input');
        errorMessage.hide();
    });
});
