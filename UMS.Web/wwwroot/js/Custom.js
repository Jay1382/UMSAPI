$(document).ready(function () {
    loadEmployeeList();
    $("#myModel").click(function () {
        $("#Id").val("Id");
        $("#FirstName").val("");
        $("#LastName").val("");
        $("#DepartmentName").val("0");
        $('#btnUpdate').hide();
        $('#btnAdd').show();
        $('#modalLabel').text('Add Employee');
        $("#myModal").modal('show');
    });
    GetDepartment();
});

function loadEmployeeList() {
    $.ajax({
        url: "/Employee/GetEmployees",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr><td>' + item.id + '</td>'
                    + '<td>' + item.firstName + '</td>'
                    + '<td>' + item.lastName + '</td>'
                    + '<td>' + item.departmentName + '</td>'
                    + '<td>' + item.departmentId + '</td>'
                    + '<td><a href="#" onclick="return getEmployeeById(' + item.id + ')">Edit</a> | <a href="#" onclick="return DeleteEmployee(' + item.id + ')">Delete</a></td>'
                    + '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function GetDepartment() {
    $.ajax({
        url: "/Employee/GetDepartment",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#DepartmentName").empty();
            $("#DepartmentName").append('<option value="0"> -- Select Department -- </option>');
            $.each(data, function (index, row) {
                $("#DepartmentName").append("<option value='" + row.id + "'>" + row.departmentName + "</option>")
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getEmployeeById(Id) {
    var data = {
        Id: Id
    };
    $.ajax({
        url: "/Employee/GetEmployeeById",
        data: data,
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#Id").val(result.id);
            $("#FirstName").val(result.firstName);
            $("#LastName").val(result.lastName);
            $("#DepartmentName").val(result.departmentId);
            $('#modalLabel').text('Update Employee');
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteEmployee(Id) {
    var confirmRes = confirm("Are you sure want to delete this employee?");
    if (confirmRes) {
        var data = {
            Id: Id
        };
        $.ajax({
            url: "/Employee/DeleteEmployee",
            data: data,
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadEmployeeList();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function AddEmployee() {
    var data = {
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        DepartmentId: $("#DepartmentName").val()
    };
    $.ajax({
        url: "/Employee/AddEmployee",
        data: data,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == 1) {
                $('#myModal').modal('hide');
                loadEmployeeList();
            }
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function UpdateEmployee() {
    var data = {
        Id: $("#Id").val(),
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        DepartmentId: $("#DepartmentName").val(),
    };
    $.ajax({
        url: "/Employee/UpdateEmployee",
        data: data,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            loadEmployeeList();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function CloseModel() {
    $('#myModal').modal('hide')
}