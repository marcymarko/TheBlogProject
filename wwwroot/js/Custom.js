let index = 0;

function AddTag() {
    //Get a reference to the tagEntry by Id
    var tagEntry = document.getElementById("TagEntry");

    //Create a ne Select Option
    let newOption = new Option(tagEntry.value, tagEntry.value);
    document.getElementById("TagList").options[index++] = newOption;

    //Clear out the eTagEntry control
    tagEntry.value = "";
    return true;

}


function DeleteTag() {
    let tagCount = 1;
    while (tagCount > 0) {
        let tagList = document.getElementBtId("TagList");
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {
            tagList.options[selectedIndex] = null;
            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }

}


$("form").on("submit", function () {
    $("TagList option").prop("selected", "selected");
})