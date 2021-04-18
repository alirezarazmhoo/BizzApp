
function checkvalidity(target) {
    var ErrorText = '';
    var state = 1;
    var count = 0;
    $('form#' + target + '').find('input').each(function () {
        if ($(this).prop('required') && $(this).val() === "") {
            count++;
            ErrorText += $(this).attr('display') + " ، ";
            state = 0;
        }
    });
    if (state === 0) {

        if (count > 1) {

            ErrorText += 'خالی هستند!';
        }
        else {
            ErrorText += 'خالی است!';
        }
        $("#textError").text(ErrorText);
        $("#ErrorModal").modal('show');
        return 0;
    }
    else {
        return 1;
    }
}
function clear(target, InputType) {

    for (var i = 0; i < InputType.length; i++) {

        $('form#' + target + '').find(InputType[i]).each(function () {
            $(this).val('');
        });
    }
}
function cleardiv(target) {

    for (var i = 0; i < target.length; i++) {
        $('#' + target[i] + '').html('');
    }
}
function CreateModal(target, mode) {
    var modal = "";
    if (parseInt(mode) === 1) {
        modal = "<div class='modal fade' id='ErrorModal' role='dialog'><div class='modal-dialog modal-sm'><div class='modal-content'><div class='modal-header'><h4 class='modal-title text-danger'>خطا</h4></div><div class='modal-body text-danger'><p id='textError'></p></div><div class='modal-footer'><button type='button' class='btn btn-danger'  data-dismiss='modal'>بستن</button>";
        document.getElementById(target).innerHTML = modal;
    }
    else if (parseInt(mode) === 2) {
        modal = "<div class='modal fade' id='SuccessModal' role='dialog'><div class='modal-dialog modal-sm'><div class='modal-content'><div class='modal-header'><h4 class='modal-title text-success'> موفقیت آمیز</h4></div><div class='modal-body text-success'><p>عملیات انجام شد!</p></div><div class='modal-footer'><button type='button' class='btn btn-success'  data-dismiss='modal'>تایید</button>";
        document.getElementById(target).innerHTML = modal;
    }
    else {
        modal = "<div class='modal fade' id='QuestionModal' role='dialog'><div class='modal-dialog modal-sm'><div class='modal-content'><div class='modal-header'><h4 class='modal-title'>پرسش</h4></div><div class='modal-body text-warning'><p> آیا مطمئن هستید?</p></div><div class='modal-footer'><button type='button' class='btn btn-success' onclick='Remove();' data-dismiss='modal'>تایید</button><button  style='margin-left:5px' type='button' class='btn btn-danger' data-dismiss='modal'>انصراف</button>";
        document.getElementById(target).innerHTML = modal;
    }
}
function PostAjax(ActionName, Parameters, redirecturl) {
    
    var fd = new FormData();
    for (var i = 0; i < Parameters.length; i++) {
        if (Parameters[i].special === 'combo') {
            fd.append(Parameters[i].id, $('#' + Parameters[i].htmlname + '').find('option:selected').val());
        }
        else if (Parameters[i].special === 'Multicombo') {
            fd.append(Parameters[i].id, $('#' + Parameters[i].htmlname + '').val());
        }
        else if (Parameters[i].special === 'radio') {
            fd.append(Parameters[i].id, $('input[name="' + Parameters[i].htmlname + '"]:checked').val());
        }
        else if (Parameters[i].special === 'file') {

            $.each($(".TheFile"), function (i, obj) {
                $.each(obj.files, function (j, file) {
                    fd.append("file", file);
                });
            });

            $.each($(".TheFile2"), function (i, obj) {
                $.each(obj.files, function (j, file) {
                    fd.append("featureImage", file);
                });
            });

        }
        else if (Parameters[i].special === 'music') {
            $.each($(".MusicUrl"), function (i, obj) {
                $.each(obj.files, function (j, file) {
                    fd.append("musicfiles", file);
                });
            });
        }
        else if (Parameters[i].special === 'siglefile') {
            fd.append("pictiremusic", $('#' + Parameters[i].htmlname + '')[0].files[0]);
        }
        else {
           
            fd.append(Parameters[i].id, $('#' + Parameters[i].htmlname + '').val());
        }
    }
    $.ajax({
        type: "POST",
        url: "" + ActionName + "",
        data: fd,
        dataType: "json",
        contentType: false,
        processData: false,
        beforeSend: function () {
            $("#LoadingModal").modal('show');
        },
        success: function (response) {
            if (response.success) {
                $("#SuccessModal").modal('show');

                setTimeout(function () { location.href = redirecturl; }, 2000);
            }
            else {
                $("#textError").text(response.responseText);
                $("#ErrorModal").modal('show');
            }
        },
        error: function (response) {
            $("#LoadingModal").modal('show');
        },
        complete: function () {
            $("#LoadingModal").modal('toggle');
        }
    });
}
function CreateAllModals() {
    CreateModal('Error', 1);
    CreateModal('Success', 2);
    CreateModal('Question', 3);
}

function FillComboBox(ActionName, Target) {
    $.ajax({
        type: "GET",
        url: "" + ActionName + "",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                $.each(response.list, function () {

                    $('#' + Target + '').append($("<option/>").val(this.id).text(this.name));
                });
            }
            else {
                $("#textError").text(response.responseText);
                $("#ErrorModal").modal('show');
            }
        },
        error: function (response) {
            alert("Error");
            //$("#LoadingModal").modal('show');
        }
    });
}

function EditAjax(ActionName, id, onSuccess = null) {
    
    var fd = new FormData();
    fd.append('ItemId', id);
    $.ajax({
        type: "Post",
        url: "" + ActionName + "",
        data: fd,
        dataType: "json",
        contentType: false,
        processData: false,
        //beforeSend: function () {
        //    $("#LoadingModal").modal('show');
        //},
        success: function (response) {
            
            if (response.success) {
                $.each(response.listItem, function () {                 
                    $('#' + this.key + '').val(this.value);
                });   
                if (response.statusitem != null) {

                    $.each(response.statusitem, function () {
                        $("#IsEnabled option[value=" + this.key + "]").attr("selected", true);
                    });
                }

                if (response.featuretype != null) {
                    
                    $.each(response.featuretype, function () {
                       
                        $("#FeatureType option[value=" + this.key + "]").attr("selected", true);
                    });
                }

                // 
                if (onSuccess !== null) {
                    onSuccess();
                }

                $('#myModal').modal('show');
            }
            else {
                $("#textError").text(response.responseText);
                $("#ErrorModal").modal('show');
            }
        },
        error: function (response) {
            $("#LoadingModal").modal('show');
        }
        //complete: function () {
        //    $("#LoadingModal").modal('toggle');
        //}
    });


}
function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    });
}

function SetInputFilter(targets) {
    for (var i = 0; i < targets.length; i++) {
        setInputFilter(document.getElementById(targets[i]), function (value) {
            return /^\d*\.?\d*$/.test(value);
        });
    }
}
function ResetListBox(targets) {

    for (var i = 0; i < targets.length; i++) {
        $('#' + targets[i] + '').prop('selectedIndex', 0);
    }

}


function SetPictures(inputtarget, target, type) {
    var myURL = window.URL || window.webkitURL;
    var result = "";
    var tag = "";
    var _File = document.getElementById("" + inputtarget + "").files;
    for (var i = 0; i < _File.length; i++) {
        var fileURL = myURL.createObjectURL(_File[i]);

        if (type == 'music') {
            tag = "<img src='../Images/MusicIcon.png' style='width:80px;height:60px;'>";
        }
        else {
            tag = "<img src='" + fileURL + "' style='width:80px;height:60px;'>";
        }
        result += tag;
    }

    $('#' + target + '').html(result);
}


function RemoveFiles(ParentTarget, ActionName, ParameterName, Parametervalue) {

    var fd = new FormData();

    fd.append(ParameterName, Parametervalue);
    $.ajax({
        type: "POST",
        url: "" + ActionName + "",
        data: fd,
        dataType: "json",
        contentType: false,
        processData: false,
        beforeSend: function () {

            $('.btnremovefile').prop('disabled', true);
        },
        success: function (response) {

        },
        error: function (response) {
            $("#LoadingModal").modal('show');

        },
        complete: function () {
            $('.btnremovefile').prop('disabled', false);

        }
    });
}
function AjaxFillModal(ActionName, Target, mode, page) {
    $.ajax({
        type: "GET",
        url: "" + ActionName + "?pageNumber=" + page + "",
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (response) {
            if (mode === "row") {

                if (response.success) {
                    $.each(response.list, function () {
                        $('#' + Target + '').append($("<div class='row' style='padding:10px;background-color:aquamarine;width:90%;margin:auto;margin-top:5px'><p>" + this.name + "<input type='checkbox' value='" + this.id + "'  style='margin-left:2px'/></p></div>"));
                    });
                }
                else {
                    $("#textError").text(response.responseText);
                    $("#ErrorModal").modal('show');
                }
            }
            if (mode === 'table') {
                if (response.success) {
                    var rownnum = 0;

                    $.each(response.list, function () {
                        rownnum += 1;
                        $('#' + Target + ' tr:last').after('<tr><td>' + rownnum + '</td><td>' + this.name + '</td><td><input name="musicsselector" id="testm" type="checkbox" value="' + this.id + '" onchange="addToItems(this)" /></td></tr>');
                    });
                    if (response.hasNextPage !== undefined) {
                        if (response.hasNextPage === true) {

                            $("#MusicNext").prop('disabled', false);

                        }
                        else {
                            $("#MusicNext").prop('disabled', true);

                        }
                    }
                    if (response.hasPreviousPage !== undefined) {
                        if (response.hasPreviousPage === true) {
                            //$("#MusicPre").addClass("");
                            $("#MusicPre").prop('disabled', false);
                        }
                        else {
                            $("#MusicPre").prop('disabled', true);
                        }
                    }
                    if (response.pageIndex !== undefined) {
                        $("#MusicPre").attr("pageNumber", response.pageIndex - 1);
                        $("#MusicNext").attr("pageNumber", response.pageIndex + 1);
                    }
                }
                else {
                    $("#textError").text(response.responseText);
                    $("#ErrorModal").modal('show');
                }
            }
        },
        error: function (response) {
            alert("Error");
            //$("#LoadingModal").modal('show');
        }
    });
}
var array_name = [];
function addToItems(obj) {
    if ($(obj).is(":checked")) {
        var t = $(obj).attr("value");
        array_name.push(t);
    } else {
        var r = $(obj).attr("value");
        array_name.splice(array_name.indexOf(r), 1);
    }
}
function showarray() {
    for (var i = 0; i < array_name.length; i++) {
        alert(array_name[i]);
    }
}

function changePage(number, target, actionName) {
    //$("#" + target + "  tr").each(function () {
    //    var rownumber = $(this).closest("tr").index();
    //    if (rownumber !== 0) {
    //        $(this).remove();
    //    }
    //});
    //AjaxFillModal(actionName, "tableMusic", "table", number);

    //$.each("#testm", function () {
    //    alert("yes");
    //});
    alert($("input[Name=musicsselector]").val());


    //for (var i = 0; i < array_name.length; i++) {




    //}
    //$('#tableMusic input[type="checkbox"]:checked').each(function () {
    //    alert($(this).attr('value'));
    //});
}

