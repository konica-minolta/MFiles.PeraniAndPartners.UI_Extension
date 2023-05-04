// Entry point of the dashboard.
//function OnNewDashboard(dashboard) {

//    // Parent is a shell pane container (tab), when dashboard is shown in right pane.
//    var tab = dashboard.Parent;

//    // Initialize console.
//    console.initialize(tab.ShellFrame.ShellUI, "myDashboardName");

//    // Some things are ready only after the dashboard has started.
//    dashboard.Events.Register(MFiles.Event.Started, OnStarted);
//    function OnStarted() {
//        alert("Hello World");
//    }
//}
$(document).ready(function () {
    //Load Dati
   
    GetData(1,0);
     
    //Modifica Numero Pagine
    $("#itemPerPagina").on("change", function () {
        GetData(1,0);
    });

    //Click bottone ricerca
    //Modifica Numero Pagine
    $("#searchButton").on("click", function () {
        GetData(1, 0);
    });
});
function GetRecordNumber(currentPage, startPaging) {
    $.ajax({
        url: "http://localhost:5250/api/IntranetCounter",
        data: {
            pageSize: $("#itemPerPagina").val(),
            dominio: ($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null"
        },
        success: function (result) {
            
            CreatePagination(result, currentPage, startPaging);
        }
    });
}

function CreatePagination(pageCount,currentPage,startPage) {

    var count = pageCount;

    if (pageCount > 10) {
        pageCount = 10;
    }
  
    if (startPage == 1) {
        startPage = parseInt($("#pagination-perani li").eq(1).text()) + 1;
        if (startPage > 1) {
            pageCount = startPage + pageCount - 1;
        }
        
    }
    else if (startPage == -1) {
        startPage = parseInt($("#pagination-perani li").eq(1).text()) - 1;
        if (startPage > 1) {
            pageCount = startPage + pageCount - 1;
        }
       
    }
    else if (startPage == 0) {
        startPage = 1;
    }
    else {
        startPage = parseInt($("#pagination-perani li").eq(1).text());
        if (startPage > 1) {
            pageCount = startPage + pageCount-1;
            //alert(startPage + "-" + pageCount);
        }
    }

    $("#pagination-perani li").remove();
    $("#pagination-perani").append('<li id="prevPage" class="page-item"><a class="page-link" href="#" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>');
    for (var i = startPage-1; i < pageCount; i++) {
        $("#pagination-perani").append('<li class="page-item item"><a class="page-link" href="#">' + (i + 1) + '</a></li>');
       
        if ((i + startPage) == currentPage) {
            //alert((i + startPage) + "-" +i+"-" +currentPage);
            $("#pagination-perani li.item").eq(i).addClass("current");
        }
        
    }
    $("#pagination-perani").append(' <li id="nextPage" class="page-item"><a class="page-link" href="#" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li>');
    
    //$("#pagination-perani li").first().find("a").addClass("current");

    $("#pagination-perani li#nextPage a").on("click", function () {
        
        if (parseInt($("#pagination-perani li").eq($("#pagination-perani li").length - 2).text()) < count) {
            CreatePagination(count, parseInt($(this).text()), 1);
        }
    });
    $("#pagination-perani li#prevPage a").on("click", function () {
        if (parseInt($("#pagination-perani li").eq(1).text())> 1) {
            CreatePagination(count, parseInt($(this).text()), -1);
        }
       
    });
    $("#pagination-perani li.item a").on("click", function () {
        GetData(parseInt($(this).text()));
    });
    
}
function GetData(currentPage,startPaging) {

    GetRecordNumber(currentPage, startPaging);

    $.ajax({
        url: "http://localhost:5250/api/Intranet",
        data: {
            currentPage: currentPage,
            pageSize: $("#itemPerPagina").val(),
            dominio: ($("#nomeDominio").val() != "") ? $("#nomeDominio").val():"null",
        },
        success: function (result) {
            // convert string to JSON


            $(function () {
                $("#IntranetTable tr.tableRow").remove();
                $.each(result, function (i, item) {
                    var tr = $('<tr>').append(
                        $('<td>').text(item.nomeDominio),
                        $('<td>').text(item.dataRegistrazione),
                        $('<td>').text(item.dataScadenza),
                        $('<td>').text(item.provider),
                        $('<td>').text(item.registrar),
                        $('<td>').text(item.cliente),
                        $('<td>').text(item.owner),
                        $('<td>').text(item.noteExRegistrar),
                        $('<td>').text(item.noteGenerali)
                    );
                    tr.addClass("tableRow");
                    $('#IntranetTable tbody').append(tr);
                });
            });
        }
    });
}