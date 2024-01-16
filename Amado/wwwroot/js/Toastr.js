toastr.options = {
    "closeButton": true,
    "newestOnTop": false,
    "progressBar": true,
    //"positionClass": "toast-bottom-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(document).ready(function () {
    if ($("#Success").length) {
        toastr["success"]("Uğurlu əməliyyat!", "")
    }
    else if ($("#Error").length) {
        toastr["error"]("Bu Email Movcuddur...")
    }
})