function openLink(culture) {
    var url = $('[data-sf-role="' + culture + '"]').val();
    window.location = url;
}