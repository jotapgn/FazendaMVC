// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function tamanhoMaximoCampoNumerio(item) {
    var id = item.id;
    var inputAlterado = document.getElementById(id);
    inputAlterado.value = inputAlterado.value.replace(/[^0-9]/gi, "");
} 
function adicionarNovaTag() {
    var elements = document.getElementsByClassName("form-control tag")
    var count = elements.length
    var novoInput = document.createElement("input")
    var novoLabel = document.createElement("label") 
    novoInput.setAttribute("for", "z" + count + "__Tag")
    novoInput.setAttribute("maxlength", "15")
    novoInput.setAttribute("class", "form-control tag")
    novoInput.setAttribute("oninput", "tamanhoMaximoCampoNumerio(this)")
    novoInput.setAttribute("name", "[" + count + "].Tag")
    novoInput.setAttribute("id", "z" + count + "__Tag")

    novoLabel.setAttribute("for", "z" + count + "__Tag");
    novoLabel.setAttribute("class", "control-label");
    novoLabel.innerHTML = "Tag"

    var breakHtml = '<br />';
    $('#divTags').append(novoLabel);
    $('#divTags').append(breakHtml);
    $('#divTags').append(novoInput);

} 