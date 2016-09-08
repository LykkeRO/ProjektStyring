$(document).ready(function () {
    //Laver en click funktion til knappen med btnCreate
    $('#btnCreate').on("click", function () {
        //Tager formens input-felter, og generere en streng
        var data = $('#createCategoryForm').serialize();

        //Laver et ajaxs kald
        $.ajax({
            url: "api/category", //laver et kald til URL adressen
            method: "POST", //Måden man laver kaldet på
            data: data, //Data som vi tager med oppe fra variablen data
            //headers: {
            //    "Authorization": "Bearer " + logindata.token
            //}

        })
          //Når vores ajax kald er gået godt, vi får et object med tilbage  
        .done(function (data) {
            $.notify("Din kategori er nu oprettet", "success");//Laver en alert, med en tekst
            console.log(data);//Vi skriver vores data ind i browserens "logbog"
            GetCategorys();//Kalder javascript-funktionen 'GetCatetory'
            $('#createCategory').modal('hide')//Finder vores element der hedder createCategory, og gemmer vores modal væk.


        });
    });
    $('#btnCreateTask').on("click", function () {
        var data = $('#createTaskForm').serialize();

        $.ajax({
            url: "api/task",
            method: "POST",
            data: data,
            //headers: {
            //    "Authorization": "Bearer " + logindata.token
            //}

        })
        .done(function (data) {
            $.notify("Din opgave er nu oprettet", "success");
            console.log(data);
            GetCategorys();
            $('#createTask').modal('hide')
   
        });
    });
    $('#createTask').on('show.bs.modal', function (e) {
        
        console.log(e.relatedTarget);
        var id = $(e.relatedTarget).data('id');


        $('#categoryId').val(id);
        // do something...
    });

    function GetCategorys() {
        $('#categorys').empty();

        var header = "";
        $.ajax
        ({
            url: "api/category"
        })
        .done(function (data) {
            $('.columns-container').empty();
            $.each(data, function (key, item) {

                var section = $('<section>');

                header = '<div class="header-column">\
  							        <header>\
    							        <div class="category">\
      							        <h2 id="Idag">'+ item.Name + '</h2>\
    							        </div>\
    							        <a class="add-inline" data-id="'+ item.Id + '" data-toggle="modal" data-target="#createTask">\
      							        <span class="glyphicon glyphicon-plus"></span>\
    							        </a>\
  							        </header>\
                                </div>';


                section.append(header);

                var tasks = $('<div>', { class: "items-column", 'data-id': item.Id });

                //Foreach start løkke
                $.each(item.Tasks, function (key, task) {

                    var taskdiv = '<div class="task-container" data-id="' + task.Id + '">\
                                <div class="task '+ (task.Finished == true ? "finished" : null) + '">\
                                    <p class="title">' + task.Name + ' </p>\
                                    <div class="icon finishTask " data-id="' + task.Id + '">\
                                        <span class="glyphicon glyphicon-ok"></span>\
                                    </div>\
                                    <div class="icon removeTask" data-id="'+ task.Id + '">\
                                        <span class="glyphicon glyphicon-remove"></span>\
                                    </div>\
                                </div>\
                                        </div>';

                    tasks.append(taskdiv);

                });
                //foreach end

                section.append(tasks);
                $('.columns-container').append(section);
            });


            $(".items-column").sortable({
                connectWith: ".items-column",
                placeholder: "ui-state-highlight",

                receive: function (event, ui) {
                    var taskID = $(ui.item).data('id');
                    var CategoryId = $(event.target).data('id');


                    $.ajax
                    ({
                        url: "api/task/" + taskID + "?CategoryId="+CategoryId,
                        method: "PUT",
                    }).done(function (data) {
                    });
                    

                }
            }).disableSelection();

            $('.finishTask').on("click", function () {

                var button = $(this);

                var id = $(this).data().id;
                $.ajax
                ({
                    url: "api/task/" + id,
                    method: "PUT",


                }).done(function (data) {

                    $(button).closest('.task').toggleClass("finished");
                });

            });

            $('.removeTask').on("click", function () {
                var button = $(this);

                var id = $(this).data().id;
                $.ajax
                ({
                    url: "api/task/" + id,
                    method: "DELETE",

                }).done(function (data) {

                    $(button).closest('.task-container').remove();
                });

            });


        });
    }
    GetCategorys();
});
