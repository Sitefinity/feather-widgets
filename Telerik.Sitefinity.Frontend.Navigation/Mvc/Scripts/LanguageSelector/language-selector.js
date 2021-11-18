function openLink(culture) {
    var url = document.querySelector('[data-sf-role="' + culture + '"]').value;

    window.location = url;
}