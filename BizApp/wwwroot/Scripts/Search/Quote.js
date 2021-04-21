var count = 1;
var count2 = 1;
var countPage = 1;
//vaghti ejra mishavad ke modal Quote baz mishavad
function OpenQuotes(e) {
    $("#BussinessID").val($(e).data('id'));
    countPage = 1;
    count = 1;
    count2 = 1;
    var CategoryId = $(e).data('categoryid');
    var fd = new FormData();
    fd.append('CategoryId', CategoryId);
    $.ajax({
        type: "Post",
        url: "/Search/GetByCategoryId",
        data: fd,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                var str = "";
                $.each(response.businessQoutes, function () {
                    var idBussinessQuotes = this.id;
                    str += "<div id='countain_" + count2 + "'><h6>" + this.ask + "</h6><br />";
                    if (this.isSelectedAnswer != true) {
                        str += "<div id=" + idBussinessQuotes + "><textarea onkeyup='onkeypressTextArea(this)' class='form-control' id='ans_" + count + "'></textarea></div><br/>";
                        count += 1;
                    }
                    else {
                        var array = this.answer.split("&");
                        $.each(array, function (i) {
                            //str += "<input class='form-control' type='radio' id='ans_" + count + "' name='name_" + count2 + "' value=" + array[i] + "><label for='ans_" + count + "' > " + array[i] + "</label ><br/>";
                            str += "<label id=" + idBussinessQuotes + " class='containerCheckBox'>" + array[i] + "<input type = 'radio'  id='ans_" + count + "' name = 'name_" + count2 + "' value=" + array[i] + "><span class='checkmark'></span></label>";
                            count += 1;
                        });
                    }
                    count2 += 1;
                    str += "</div>";
                });
                $('#click-quote').modal('show');
                $('#click-quote #AllQuote').html(str);
                for (var i = 1; i <= count2; i++) {
                    $("#countain_" + i + "").hide();
                }
                $("#countain_1").show();
                $("#SubmitQuotes").hide();
                $("#PrevBtn").hide();
                $("#AllQuote input:radio").click(function () {
                    SearchPagesQuotes("Next");
                });

            }
            else {
                $("#textError").text(response.responseText);
                $("#ErrorModal").modal('show');
            }
        },
        error: function (response) {
            $("#LoadingModal").modal('show');
        }
    });
}
//vaghti ke dakhele textarea ha type mikonad
function onkeypressTextArea(e) {
    if ($(e).val() != "") {
        $("#NextBtn").prop('disabled', false);
        $("#SubmitQuotes").prop('disabled', false);
    }
    else {
        $("#NextBtn").prop('disabled', true);
        $("#SubmitQuotes").prop('disabled', true);
    }
}
//vaghti ke dokme badi va ghabli ra dar quote mizanad
function SearchPagesQuotes(NextOrPrev) {
    
    $("#PrevBtn").show();
    var isEanbledSubmitButton = false;
    if (NextOrPrev == "Next") {
        var lenghtCheckedThisPage = $("#countain_" + countPage + "").has(":checked").length;
        if (lenghtCheckedThisPage != 0) {
            $("#SubmitQuotes").prop('disabled', false);
            isEanbledSubmitButton = true;
        }
        countPage += 1;
    }
    else {
        countPage -= 1;
    }
    var lenghtChecked = $("#countain_" + countPage + "").has(":checked").length;
    var textAreaContain = $("#countain_" + countPage + "").find("textarea").val();
    if (textAreaContain == "" || lenghtChecked == 0) {
        if (isEanbledSubmitButton == false) {
            $("#SubmitQuotes").prop('disabled', true);
        }
        $("#NextBtn").prop('disabled', true);
    }
    else {
            $("#SubmitQuotes").prop('disabled', false);
        $("#NextBtn").prop('disabled', false);
    }
    if (count2 != countPage) {
        if (countPage == 1)
            $("#PrevBtn").hide();
        $("#NextBtn").show();
        $("#SubmitQuotes").hide();
        for (var i = 1; i <= count2; i++) {
            $("#countain_" + i + "").hide();
        }
        $("#countain_" + countPage + "").show();
        if (countPage + 1 == count2) {
            $("#NextBtn").hide();
            $("#SubmitQuotes").show();
        }
    }
}
//vaghti ke quote ra taeed mikonad
function SubmitQuotes() {
    var AllAnswerQoute = [];
    $('#AllQuote input[type=radio]').each(function () {
        if (this.checked) {
            var ans = $(this).parent().text() + "&" + $(this).parent().attr('id');
            AllAnswerQoute.push(ans);
        }
    });
    $('#AllQuote textarea').each(function () {
        var ans = this.value + "&" + $(this).parent().attr('id');
        AllAnswerQoute.push(ans);
    });
    var BusinessId = $("#click-quote #BussinessID").val();
    $.ajax({
        type: "Post",
        url: "/Search/AddBussinessQuoteUser",
        data: { BusinessId, AllAnswerQoute },
        dataType: "json",
        success: function (response) {
            if (response.success) {
                $('#click-quote').modal('hide');
                $('#click-quote-result').modal('show');
            }
            else {
                $("#textError").text(response.responseText);
                $("#ErrorModal").modal('show');
            }
        },
        error: function (response) {
            $("#LoadingModal").modal('show');
        }
    });
}