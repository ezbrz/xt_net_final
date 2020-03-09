//actions for edit and delete
$('.action').on('click', '.btn', function () {
    var btn = $(this), element, controlElement, index = 0, deleteIndex = 0, editIndex = 0;
    controlElement = btn.closest('div').parent('div').attr('name');
    element = btn.closest('td');
    switch (btn.attr('name')) {
        case 'edit':

            if (controlElement == 'user-action') { $(location).attr('href', '/Pages/administrator/EditModerator.cshtml?id=' + element.attr('name')); }
            if (controlElement == 'product-action') { $(location).attr('href', '/Pages/administrator/EditProduct.cshtml?id=' + element.attr('name')); }
            if (controlElement == 'producer-action') { $(location).attr('href', '/Pages/administrator/EditProducer.cshtml?id=' + element.attr('name')); }
            break;

        case 'delete':
            if (controlElement == 'user-action') {
                deleteIndex = element.closest('tr').children('td[name=userName]').html();
                deleteTemplate('user', deleteIndex);
            }
            if (controlElement == 'product-action') {
                deleteIndex = element.closest('tr').children('td[name=productName]').html();
                deleteTemplate('product', deleteIndex);
            }
            if (controlElement == 'producer-action') {
                deleteIndex = element.closest('tr').children('td[name=producerName]').html();
                deleteTemplate('producer', deleteIndex);
            }
            $('.delete-btn-group .yes').on('click', function (e) {
                $('body .panel-delete').remove();
                $('.layout').hide();
                if (controlElement == 'user-action') { $(location).attr('href', '/Pages/administrator/DeleteModerator.cshtml?id=' + element.attr('name'));}
                if (controlElement == 'product-action') { $(location).attr('href', '/Pages/administrator/DeleteProduct.cshtml?id=' + element.attr('name')); }
                if (controlElement == 'producer-action') { $(location).attr('href', '/Pages/administrator/DeleteProducer.cshtml?id=' + element.attr('name')); }
            });
            $('.delete-btn-group .no').on('click', function (e) {
                $('body .panel-delete').remove();
                $('.layout').hide();
            });
            break;
    }
});
//validate input
$('.form-edit').on('blur', 'input[name=login]', function (e) {
    validate();
});

$('.form-edit').on('click', 'button', function () {
    if (switcher($('input[name=login]'))) {
        $('.form-edit').submit();
    } else { return false; };
});
