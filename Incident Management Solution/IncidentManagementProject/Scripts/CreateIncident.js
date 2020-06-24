$(document).ready(function () {
    $('#divEmployeeDetails').hide();

    $('#ddlbusinessfunction').change(function () {
        if ($('#ddlbusinessfunction').val() == "") {
            $("#ddlServices").empty();
            $('#ddlCategories').empty();
            $("#ddlServices").append($('<option/>', { value: -1, text: '--Choose--' }));
            $('#ddlCategories').append($('<option/>', { value: -1, text: '--Choose--' }));
            $('#divEmployeeDetails').hide();
            $('#ddlBusunessFunErrorMsg').show();
        }
        else {
            $.ajax({
                url: '/CreateIncident/GetIncidentServices',
                method: 'Get',
                data: { Business_id: $(this).val() },
                dataType: 'json',
                success: function (data) {
                    $('#ddlServices').empty();
                    $('#ddlServices').append($('<option/>', { value: -1, text: '--Choose--' }))
                    $(data).each(function (index, item) {
                        $('#ddlServices').append($('<option/>', { value: item.Value, text: item.Text }));
                    })
                    $('#divEmployeeDetails').show();
                    $('#ddlBusunessFunErrorMsg').hide();
                }


            });
        }
    })
    $('#ddlServices').change(function () {
        if (($('#ddlServices').val() == "") || ($('#ddlServices').val() == "-1")) {
            $('#ddlCategories').empty();
            $('#ddlCategories').append($('<option/>', { value: -1, text: '--Choose--' }));
            $('#ddlServiceErrorMsg').show();
        }
        else {
            $.ajax({
                url: '/CreateIncident/GetIncidentCategories',
                data: { Service_id: $(this).val() },
                method: 'Get',
                dataType: 'json',
                success: function (data) {
                    $(data).each(function (index, item) {
                        $('#ddlCategories').append($('<option/>', { value: item.Value, text: item.Text }))
                    })
                    $('#ddlServiceErrorMsg').hide();
                }
            });
        }
    })
    $('#ddlCategories').change(function () {
        var ddlCategory = $('#ddlCategories').val();
        if (ddlCategory == "" || ddlCategory == "-1") {
            $('#ddlCategoryErrorMsg').show();
        }
        else {
            $('#ddlCategoryErrorMsg').hide();
        }

    })

    $('#btnSubmitIncident').click(function () {
        var ddlBusinessFunction = $('#ddlbusinessfunction').val();
        var ddlService = $('#ddlServices').val();
        var ddlCategory = $('#ddlCategories').val();
        var title = $('#txtTitle').val();
        var description /*= CKEDITOR.instances['editor'].getData()*/;
        //var description = $('#txtAreaDescription').val();
        if (title != "") {
            $('#txtTitleErrorMsg').hide();
        }
        if (description != "") {
            $('#txtAreaDescriptionErrorMsg').hide();
        }
        if (ddlService == "" || ddlService == "-1") {
            $('#ddlServiceErrorMsg').html("Please select Service");
        }
        if (title == ""/* || description == ""*/ || ddlBusinessFunction == "" || ddlCategory == "" || ddlCategory == "-1") {
            $('#txtTitleErrorMsg').html("Please Provide Incident Title");
            $('#txtAreaDescriptionErrorMsg').html("Please Provide Incident Description");
            $('#ddlBusunessFunErrorMsg').html("Please select business function");
            $('#ddlCategoryErrorMsg').html("Please select Category");
            return false;
        }
        else if (title != "" /*|| description != ""*/ || ddlBusinessFunction != "" || ddlService != "" || ddlCategory != "") {
            $('#txtTitleErrorMsg').hide();
            $('#txtAreaDescriptionErrorMsg').hide();
            $('#ddlBusunessFunErrorMsg').hide();
            $('#ddlServiceErrorMsg').hide();
            $('#ddlCategoryErrorMsg').hide();
            return true;
        }

    })
    $('#FileUpload').click(function () {
        var uploadedFiles = $("#fileInput").get(0);
        var files = uploadedFiles.files;
        var formData = new FormData();

        for (var i = 0; i < files.length; i++) {
            formData.append(files[i].name, files[i]);
        }
        if (files.length <= 0) {
            $('#SucessMessage').hide();
            $('#SelectFile').html("Please Select File");
            $('#SelectFile').show();
        }
        else {
            $.ajax({
                url: '/CreateIncident/FileUpload',
                type: 'Post',
                data: formData,
                contentType: false,
                processData: false,
                success: function (message) {
                    $('#SelectFile').hide();
                    $('#SucessMessage').html("File Uploaded Sucessfully");
                    $('#SucessMessage').show();
                },
                error: function () {
                    $('#SucessMessage').hide();
                    $('#SelectFile').hide();
                    $('#ErrorMessage').html("Error while uploading files");
                    $('#ErrorMessage').show();
                }
            })
        }
    })

})