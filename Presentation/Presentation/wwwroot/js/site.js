$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
		// Remove the modal after onClick is done.
		placeholderElement.html("");
    });
});