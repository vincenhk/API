


$(document).ready(function () {
    var table = $("#table_id").DataTable({
        columnDefs: [
            {
                "orderable": false,
                "targets": 9
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'excel', 'pdf', 'colvis'
        ],
        
            "ajax": {
            "url": "https://localhost:44323/API/Employee/EmployeeMasterData",
                "dataTe": "JSON",
                "dataSrc": ""
            },
            "columns": [
                { "data" : null,
                    render: function (data, type, row, meta) {
                      return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
                { "data": "nik" },
                { "data": "fullName" },
                {
                    "data": null,
                    render: function (data, type, row) {
                        return `+62${row.phone.slice(1)}`
                    }
                },
                { "data": "email" },
                { "data": "birthDate"},
                {
                    "data": null,
                    render: function (data, type, row) {
                        return `Rp.${row.salary},00`
                    }
                },
                { "data": "universityName" },
                { "data": "degree" },
                {
                    "data": null,
                    render: function (data, type, row) {

                        return `<div class = "row">
                                <button type="button" id="" class="btn btn-primary" data-toggle="modal" data-target="#modalUpdate" onclick = "Update(${data.nik})"><span class="bi bi-pencil-fill"> </span> </button> &nbsp;
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalDelete" id = "btnDelete" onclick = "Delete(${data.nik})"><span class="bi bi-trash3-fill"> </span> </button> &nbsp;
                                </div>`
                    }
                }
            ]
    })

    table.buttons().container()
        .appendTo('#table_id .col-md-6:eq(0)');
    //Abstract call ready html form 
    getUniversity();

})

//JS Chart 
var options = {
    chart: {
        height: 280,
        type: "radialBar",
    },

    series: [67],
    colors: ["#20E647"],
    plotOptions: {
        radialBar: {
            hollow: {
                margin: 0,
                size: "70%",
                background: "#293450"
            },
            track: {
                dropShadow: {
                    enabled: true,
                    top: 2,
                    left: 0,
                    blur: 4,
                    opacity: 0.15
                }
            },
            dataLabels: {
                name: {
                    offsetY: -10,
                    color: "#fff",
                    fontSize: "13px"
                },
                value: {
                    color: "#fff",
                    fontSize: "30px",
                    show: true
                }
            }
        }
    },
    fill: {
        type: "gradient",
        gradient: {
            shade: "dark",
            type: "vertical",
            gradientToColors: ["#87D4F9"],
            stops: [0, 100]
        }
    },
    stroke: {
        lineCap: "round"
    },
    labels: ["Progress"]
};

var chart = new ApexCharts(document.querySelector("#chart"), options);

chart.render();


jQuery.validator.setDefaults({
    debug: true,
    success: "valid"
});

$("#form").validate({
    rules: {
        firstname: {
            required: true,
            minlength: 3
        },
        lastname: {
            required: true,
            minlength: 3
        },
        phone: {
            required: true,
            digits: true
        },
        birthdate: {
            required: true,
            date: true
        },
        gender: {
            required: true
        },
        salary: {
            required: true,
            minlength: 3,
            digits: true
        },
        email: {
            required: true,
            email: true
        },
        password: {
            required: true,
            minlength: 5
        },
        university: {
            required: true
        },
        degree: {
            required: true
        },
        gpa: {
            required: true,
            digits: true
        }
    },
    messages: {
        firstname: {
            required: 'Required!',
            minlength: 'Min 3 Character'
        },
        lastname: {
            required: 'Required!',
            minlength: 'Min 3 Character'
        },
        phone: {
            required:'Required!',
            digits: 'Should use digits!'
        },
        birthdate: {
            required: 'Required!',
            date: 'See Format date'
        },
        gender: {
            required: 'Required!'
        },
        salary: {
            required: 'Required!',
            digits: 'Use digits!'
        },
        email: {
            required: 'Required!',
            email: 'Should use symbol email'
        },
        password: {
            required: 'Required!',
            minlength: 'Min 5 Character!'
        },
        university: {
            required: 'Required!'
        },
        degree: {
            required: 'Required!'
        },
        gpa: {
            required: 'Required!',
            digits: 'GPA is a Number'
        }
    }
});

function myFunction() {
    var x = document.getElementById("password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function Insert() {
    const empRegister = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya


    //ini ngambil value dari tiap inputan di form nya
    empRegister.FirstName = $("#firstName").val();
    empRegister.LastName = $("#lastName").val();
    empRegister.Phone = $("#phone").val();
    empRegister.BirthDate = $("#birthdate").val();
    empRegister.Salary = Number($("#salary").val());
    empRegister.Email = $("#email").val();
    empRegister.Gender = getGender($("#gender").val());
    empRegister.Password = $("#password").val();
    empRegister.Degree = $("#degree").val();
    empRegister.GPA = $("#gpa").val();
    empRegister.University_Id = Number($("#show-univ").val());

    Swal.fire({
        title: 'Register Validation',
        text: `Register Employee`,
        showCancelButton: true,
        confirmButtonColor: "#0000FF",
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json;charset=utf-8'
            },
            type: "POST",
            url: "https://localhost:44323/API/Account/register",
            dataType: "json",
            data: JSON.stringify(empRegister)
        }).done((result) => {
            window.location.reload();
        }).fail((error) => {
            console.log(error);
        })
    })
    
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    
}

function Update(nik) {
    $.ajax({
        type: "GET",
        url: `https://localhost:44323/API/Employee/${nik}`,
        dataType: 'json'
    }).done((result) => {
        var text = "";
        var gen = getGenderTxt(result.gender);
        text = `
                <div class = "form-group" >
                    <label for="nik">NIK</label>
                    <input type="text" name="nik"  value = "${result.nik}" class="form-control" disabled  id = "unik">
                </div>

                <div class = "form-group" >
                    <label for="firstname">First Name</label>
                    <input type="text" name="firstname"  value = "${result.firstName}" class="form-control" id = "u-firstName">
                </div>

                <div class = "form-group">
                    <label for="lastname">Last Name</label>
                    <input type="text" name="firstname" value = "${result.lastName}" class="form-control" id = "u-lastName">
                </div>

                <div class = "form-group" >
                    <label for="phone">Phone</label>
                    <input type="text" name="phone" value = "${result.phone}" disabled class="form-control" id = "u-phone">
                </div>
                <div class = "form-group" >
                    <label for="BirthDate"> Birth Date </label>
                    <input type="text" name="BirthDate" disabled value = "${result.birthDate}" class="form-control" id = "u-birthdate">
                </div>

                <div class = "form-group">
                    <label for="Salary">Salary</label>
                    <input type="text" name="Salary" value = "${result.salary}" class="form-control" id = "u-salary">
                </div>

                <div class = "form-group">
                    <label for="Email">Email</label>
                    <input type="text" name="Email" value = "${result.email}" class="form-control" id = "u-email">
                </div>

                <div class = "form-group">
                    <label for="Gender">Gender</label>
                    <input type="text" name="Gender" value = "${gen}" disabled class="form-control" id = "u-gender">
                </div>
                `;

        $('#form-id').html(text);
    }).fail((error) => {
        console.log(error);
    })
}

function updateEmp() {
    Swal.fire({
        title: 'Edit Biodata?',
        text: `Sure data with ${nik} will Edit?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: "#0000FF",
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {

            const empUpdt = new Object();

            empUpdt.NIK = $("#unik").val();
            empUpdt.FirstName = $("#u-firstName").val();
            empUpdt.LastName = $("#u-lastName").val();
            empUpdt.Phone = $("#u-phone").val();
            empUpdt.BirthDate = $("#u-birthdate").val();
            empUpdt.Salary = Number($("#u-salary").val());
            empUpdt.Email = $("#u-email").val();
            empUpdt.Gender = getGender($("#u-gender").val());


            $.ajax({
                type: 'PUT',
                url: `https://localhost:44323/API/Employee`,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(empUpdt)
            }).done((update => {
                console.log("berhasil update");
            })
            ).fail((error) => {
                console.log(error);
            })
            window.location.reload();
        }
    })
}

function getGender(code) {
    var gender = 0;
    if (code == "Pria") {
        gender = 0;
        return gender;
    } else if (code == "Wanita") {
        gender = 1;
        return gender;
    }
}

function getGenderTxt(code) {
    var gender = "";
    if (code == 0) {
        gender = "Pria";
        return gender;
    } else if (code == 1) {
        gender = "Wanita";
        return gender;
    }
}


function getUniversity() {
    $.ajax({
        url: "https://localhost:44323/Api/University",
        async: false
    }).done((univ => {
        var text = "";
        var i = 1;
        $.each(univ, function (key, val) {
            text += `<option value ="${i}">${val.name}</option>`;
            i++;
        })
        $('#show-univ').html(text);
    })
    )
}

function Delete(nik) {
    Swal.fire({
        title: 'Delete Employee?',
        text: `Sure data with ${nik} will deleted?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: "#0000FF",
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'delete',
                url: `https://localhost:44323/API/Employee/${nik}`,
                async: false
            }).done((deleted => {
                Swal.fire(
                    'Deleted Employee',
                ).then((result) => {
                    window.location.reload();
                })
            })
            ).fail((error) => {
                console.log(error);
            })
        }
    }) 
}

//$("#form").validate({
//    rules:{
//        firstname: {
//            minlength: 2,
//            messages: {
//                required: "Please Enter your name",
//                minlength: "Name "
//            }
//        }
//    }

//    submitHandler: function (form) {
//        form.submit();
//    }
//});

//$.ajax({//
//    url: "https://localhost:44323/API/Employee/EmployeeMasterData/",
//    success: function (result) {
//        var text = "";
//        $.each(result, (function (key, val) {
//            text += ` <tr>
//                        <td>${key + 1}</td>
//                        <td>${val.nik}</td>
//                        <td>${val.fullName}</td>
//                        <td>${val.phone}</td>
//                        <td>${val.email}</td>
//                        <td>${val.birthDate}</td>
//                        <td>Rp.${val.salary}</td>
//                        <td>${val.universityName}</td>
//                        <td>${val.degree}</td>
//                    </tr>`;
//            })
//        )
//        console.log(url);
//        $('#table_id').DataTable();
//});


//$(document).ready(function () {
//    var table = $('#table_id').DataTable({
//        lengthChange: false,
//        buttons: ['copy', 'excel', 'pdf', 'colvis']
//    });

//    table.buttons().container()
//        .appendTo('#table_id_wrapper .col-md-6:eq(0)');
//});