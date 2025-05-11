showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    location.reload()
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}


// showmodal.js - Versión mejorada
(function (window) {
    'use strict';

    const deleteHandler = {
        openDeleteModal: function (triggerClass, confirmButtonId, baseUrl, callback) {
            $(document).off('click', '.' + triggerClass).on('click', '.' + triggerClass, function (e) {
                e.preventDefault();
                const id = $(this).data('id');
                const $modal = $('#deleteDialog');
                const $btnConfirm = $modal.find('#' + confirmButtonId);

                // Resetear eventos previos
                $btnConfirm.off('click');

                $modal.modal('show');

                $btnConfirm.on('click', function () {
                    const $btn = $(this);
                    $btn.prop('disabled', true)
                        .html('<span class="spinner-border spinner-border-sm"></span> Procesando...');

                    $.ajax({
                        url: baseUrl + '?id=' + id, // Ruta actualizada
                        type: 'POST',
                        dataType: 'json'
                    })
                        .done(function (data) {
                            $modal.modal('hide');
                            if (typeof callback === 'function') {
                                callback(data);
                            }
                        })
                        .fail(function (xhr) {
                            const errorMsg = xhr.responseJSON?.message ||
                                'Error en la solicitud: ' + xhr.statusText;
                            if (typeof callback === 'function') {
                                callback({ success: false, message: errorMsg });
                            }
                        })
                        .always(function () {
                            $btn.prop('disabled', false).html('Eliminar');
                        });
                });
            });
        }
    };

    // Exponer al ámbito global
    window.sc_deleteDialog = deleteHandler;

})(window);