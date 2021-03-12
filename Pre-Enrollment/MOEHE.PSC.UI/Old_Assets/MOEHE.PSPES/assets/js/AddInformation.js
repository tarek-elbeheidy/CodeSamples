var regex = /^(.+?)(\d+)$/i;
var cloneIndex = $(".myInfos").length;

function clone(){
	
    cloneIndex++;
    $("#div_1").clone()
        .appendTo($("#listInfos"))
        .attr("id", "div_" +  cloneIndex)
        .find("*")
        .each(function() {
            var id = this.id || "";
            var match = id.match(regex) || [];
            if (match.length == 3) {
                this.id = match[1] + "_" + (cloneIndex);
            }
        })
        .on('click', 'a.remove', remove);
}
function remove(){
    $(this).parents(".myInfos").remove();
}
$("a.ajouter").on("click", clone);

$("a.remove").on("click", remove);