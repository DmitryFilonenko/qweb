$(document).ready(function () {

    var file;
    var txt;

$('#submitButton').prop('disabled', true);

    function buttonState() {
        if (txt >= 0 && txt <= 255 && txt != "" && file != null) {
            $('#submitButton').prop('disabled', false);
        }
        else {
            $('#submitButton').prop('disabled', true);
        }
    }

$('#inputFile').on("change", function () {
        file = $(this).val();
        buttonState();

    });

$('#myText').keyup(function () {
        txt = $(this).val();
        buttonState();
    });
});