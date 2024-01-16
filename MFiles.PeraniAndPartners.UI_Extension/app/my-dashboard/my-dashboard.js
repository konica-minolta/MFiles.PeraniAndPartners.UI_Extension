

function OnNewDashboard(dashboard) {

    
 
    // Parent is a shell pane container (tab), when dashboard is shown in right pane.
    var tab = dashboard.Parent;

    // Initialize console.
    //console.initialize(tab.ShellFrame.ShellUI, "myDashboardName");

    // Some things are ready only after the dashboard has started.
    dashboard.Events.Register(MFiles.Event.Started, OnStarted);

    function OnStarted() {

        var accountName = dashboard.CustomData.AccountName;
      
        //Load Dati
        GetData(1, 0);

        //Modifica Numero Pagine
        $("#itemPerPagina").on("change", function () {
            GetData(1, 0);
        });

        //Click bottone ricerca
        //Modifica Numero Pagine
        $("#searchButton").on("click", function () {
            GetData(1, 0);
        });

        //EsportaExcel Pagina
        $("#EsportaPagina").on("click", function () {
            var scadenzaDal = ($("#scadenzaDal").val() != "") ? ($("#scadenzaDal").val().split('/')[1] + "/" + $("#scadenzaDal").val().split('/')[0] + "/" + $("#scadenzaDal").val().split('/')[2]) : "";
            var scadenzaAl = ($("#scadenzaAl").val() != "") ? ($("#scadenzaAl").val().split('/')[1] + "/" + $("#scadenzaAl").val().split('/')[0] + "/" + $("#scadenzaAl").val().split('/')[2]) : "";
            var currentPage = $("li.current").text();
            if (currentPage == "") currentPage = 1;

            //window.location = "http://192.168.10.242:8085/api/IntranetDataPaginated?currentPage=" + currentPage + "&pageSize=" + $("#itemPerPagina").val() + "&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + $("#scadenzaDal").val()) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + $("#scadenzaAl").val()) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
            window.location = "http://localhost:8085/api/IntranetDataPaginated?currentPage=" + currentPage + "&pageSize=" + $("#itemPerPagina").val() + "&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + scadenzaAl) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + scadenzaAl) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
        });

        //EsportaExcel Tutti
        $("#EsportaTutti").on("click", function () {
            var scadenzaDal = ($("#scadenzaDal").val() != "") ? ($("#scadenzaDal").val().split('/')[1] + "/" + $("#scadenzaDal").val().split('/')[0] + "/" + $("#scadenzaDal").val().split('/')[2]) : "";
            var scadenzaAl = ($("#scadenzaAl").val() != "") ? ($("#scadenzaAl").val().split('/')[1] + "/" + $("#scadenzaAl").val().split('/')[0] + "/" + $("#scadenzaAl").val().split('/')[2]) : "";

            //window.location = "http://192.168.10.242:8085/api/IntranetDataAll?accountName=admin&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + $("#scadenzaDal").val()) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + $("#scadenzaAl").val()) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
            window.location = "http://localhost:8085/api/IntranetDataAll?accountName=admin&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + scadenzaDal) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + scadenzaAl) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
        });

        $("#ClearButton").on("click", function () {
            $("#nomeDominio").val("");
            $("#extDominio").val("");
            $("#dominioEsatto").prop("checked", false);
            $("#tipoRicerca").val("QUALSIASI");
            $("#scadenzaDal").val("");
            $("#scadenzaAl").val("");
            $("#tipoRicerca").val("Dominio");
        });

        $(".fas").hide();
        $(".fas").first().show();

        $("#sortDominio").on("click", function () {
            if ($("#sortDominio .fa-sort-down").is(':visible')) {
                $(".fas").hide();
                $("#sortDominio .fa-sort-up").show();
                $("#recordsOrderType").text("2");
            }
            else {
                $(".fas").hide();
                $("#sortDominio .fa-sort-down").show();
                $("#recordsOrderType").text("1");
            }
            GetData(1, 0);
        });
        $("#sortDataRegistrazione").on("click", function () {
            if ($("#sortDataRegistrazione .fa-sort-down").is(':visible')) {
                $(".fas").hide();
                $("#sortDataRegistrazione .fa-sort-up").show();
                $("#recordsOrderType").text("4");
            }
            else {
                $(".fas").hide();
                $("#sortDataRegistrazione .fa-sort-down").show();
                $("#recordsOrderType").text("3");
            }
            GetData(1, 0);
        });

        $("#sortDataScadenza").on("click", function () {
            if ($("#sortDataScadenza .fa-sort-down").is(':visible')) {
                $(".fas").hide();
                $("#sortDataScadenza .fa-sort-up").show();
                $("#recordsOrderType").text("6");
            }
            else {
                $(".fas").hide();
                $("#sortDataScadenza .fa-sort-down").show();
                $("#recordsOrderType").text("5");
            }
            GetData(1, 0);
        });
    }
}


function CreatePagination(pageCount,currentPage,startPage) {

    var count = pageCount;

    if (pageCount > 10) {
        pageCount = 10;
    }
    else {
        pageCount = pageCount + 1;
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
        }
    }

    $("#pagination-perani li").remove();
    $("#pagination-perani").append('<li id="prevPage" class="page-item"><a class="page-link" href="#" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>');
    for (var i = startPage - 1; i < pageCount; i++) {
      
        if ( i == currentPage-1) {
            
            $("#pagination-perani").append('<li class="page-item item current"><a class="page-link" href="#">' + (i + 1) + '</a></li>');
        }
        else {
            $("#pagination-perani").append('<li class="page-item item"><a class="page-link" href="#">' + (i + 1) + '</a></li>');
        }
      
        
    }
    $("#pagination-perani").append(' <li id="nextPage" class="page-item"><a class="page-link" href="#" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li>');
    
    //$("#pagination-perani li").first().find("a").addClass("current");

    $("#pagination-perani li#nextPage a").on("click", function (e) {
        e.preventDefault();
        if (parseInt($("#pagination-perani li").eq($("#pagination-perani li").length - 2).text()) <= count) {
            CreatePagination(count, parseInt($(this).text()), 1);
        }
    });
    $("#pagination-perani li#prevPage a").on("click", function (e) {
        e.preventDefault();
        if (parseInt($("#pagination-perani li").eq(1).text())> 1) {
            CreatePagination(count, parseInt($(this).text()), -1);
        }
       
    });
    $("#pagination-perani li.item a").on("click", function () {
        GetData(parseInt($(this).text()));
    });

    $(window).off("keydown");
    $(window).on("keydown", function (e) {
        if (e && e.keyCode == 13) {
           
            GetData(1, 0);
            e.stopPropagation();
            e.cancelBubble = true;
        }
    });
    
}



function GetData(currentPage,startPaging) {
  
    //GetRecordNumber(currentPage, startPaging);
    var scadenzaDal = ($("#scadenzaDal").val() != "") ? ($("#scadenzaDal").val().split('/')[1] + "/" + $("#scadenzaDal").val().split('/')[0] + "/" + $("#scadenzaDal").val().split('/')[2]) : "";
    var scadenzaAl = ($("#scadenzaAl").val() != "") ? ($("#scadenzaAl").val().split('/')[1] + "/" + $("#scadenzaAl").val().split('/')[0] + "/" + $("#scadenzaAl").val().split('/')[2]) : "";
    $.ajax({
        url: "http://localhost:8085/api/Intranet",
        //url: "http://192.168.10.242:8085/api/Intranet",
        data: {
            currentPage: currentPage,
            pageSize: $("#itemPerPagina").val(),
            dominio: ($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null",
            cliente: ($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null",
            estensione: ($("#extDominio").val() != "") ? $("#extDominio").val() : "null",
            ricercaEsatta: ($("#dominioEsatto").is(':checked')) ? true : false,
            scadenzaDal: ($("#scadenzaDal").val() != "") ? scadenzaDal : null,
            scadenzaAl: ($("#scadenzaAl").val() != "") ? scadenzaAl : null,
            stato: ($("#status").val() != "") ? $("#status").val() : "null",
            order: $("#recordsOrderType").text(),
            tipoRicerca: $("#tipoRicerca").val() 
        },
        success: function (result) {

            totalRecords = parseInt(result.totalRecords);

           
            CreatePagination(parseInt(totalRecords / parseInt($("#itemPerPagina").val())), currentPage, startPaging);
            $("#recordsCounter").text(totalRecords);
            $("#IntranetTable tr.tableRow").remove();
          
            $.each(result.list, function (i, item) {
               
                var dataRegistrazione = "";
                var dataScadenza = "";
                if (item.dataRegistrazione != null && item.dataRegistrazione!="") {
                    var dataRegistrazioneTemp = item.dataRegistrazione.split('T')[0];
                    var dataRegistrazione = dataRegistrazioneTemp.split('-')[2] + "/" + dataRegistrazioneTemp.split('-')[1] + "/" + dataRegistrazioneTemp.split('-')[0];
                }
                if (item.dataScadenza != null && item.dataScadenza != "") {
                    dataScadenzaTemp = item.dataScadenza.split('T')[0];
                    var dataScadenza = dataScadenzaTemp.split('-')[2] + "/" + dataScadenzaTemp.split('-')[1] + "/" + dataScadenzaTemp.split('-')[0];
                }

                var tr = $('<tr>').append(
                    $('<td>').text(item.nomeDominioCompleto),
                    $('<td>').text(dataRegistrazione),
                    $('<td>').text(dataScadenza),
                    $('<td>').text(item.provider),
                    $('<td>').text(item.registrar),
                    $('<td>').text(item.cliente),
                    $('<td>').text(item.owner),
                    $('<td>').text(item.numeroPratica),
                    $('<td>').text(item.stato),
                    $('<td>').text(item.noteExRegistrar),
                    $('<td>').text(item.noteGenerali)
                );
                tr.addClass("tableRow");
                $('#IntranetTable tbody').append(tr);
              
            });
        }
    });
}