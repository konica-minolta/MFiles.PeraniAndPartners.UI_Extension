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
        var currentPage = $("li.current").text();
        if (currentPage == "") currentPage = 1;
        //window.location = "http://192.168.10.242:8085/api/IntranetDataPaginated?currentPage=" + currentPage + "&pageSize=" + $("#itemPerPagina").val() + "&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + $("#scadenzaDal").val()) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + $("#scadenzaAl").val()) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
        window.location = "http://localhost:8085/api/IntranetDataPaginated?currentPage=" + currentPage + "&pageSize=" + $("#itemPerPagina").val() + "&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + $("#scadenzaDal").val()) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + $("#scadenzaAl").val()) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
    });

    //EsportaExcel Tutti
    $("#EsportaTutti").on("click", function () {
        //window.location = "http://192.168.10.242:8085/api/IntranetDataAll?accountName=admin&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + $("#scadenzaDal").val()) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + $("#scadenzaAl").val()) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
        window.location = "http://localhost:8085/api/IntranetDataAll?accountName=admin&dominio=" + (($("#nomeDominio").val() != "") ? $("#nomeDominio").val() : "null") + "&estensione=" + (($("#extDominio").val() != "") ? $("#extDominio").val() : "null") + "&ricercaEsatta=" + (($("#dominioEsatto").is(':checked')) ? true : false) + (($("#scadenzaDal").val() != "") ? ("&scadenzaDal=" + $("#scadenzaDal").val()) : "") + (($("#scadenzaAl").val() != "") ? ("&scadenzaAl=" + $("#scadenzaAl").val()) : "") + "&stato=" + (($("#status").val() != "") ? $("#status").val() : "null");
    });

        $("#ClearButton").on("click", function () {
            $("#nomeDominio").val("");
            $("#extDominio option").prop("selected", false)
            $("#extDominio option[value=EMPTY_VALUE]").prop('selected', true); 
            $("#extDominio").trigger("change");
            $("#dominioEsatto").prop("checked", false);
            $("#tipoRicerca").val("QUALSIASI");
            $("#scadenzaDal").val("");
            $("#scadenzaAl").val("");
            $("#tipoRicerca").val("Dominio");
            $("#status option").prop('selected', false); 
            $("#status option[value=QUALSIASI]").prop('selected', true); 
            $("#status").trigger("change");
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

    
});