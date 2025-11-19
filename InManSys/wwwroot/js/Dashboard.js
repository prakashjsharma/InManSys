$(document).ready(function () {

    /* =====================
       CHART 1: Category Chart
       ===================== */
    const categoryCtx = document.getElementById("categoryChart");
    if (categoryCtx) {
        new Chart(categoryCtx, {
            type: 'pie',
            data: {
                labels: window.dashboardData.catLabels,
                datasets: [{
                    label: "Categories",
                    data: window.dashboardData.catValues
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            usePointStyle: true
                        }
                    },
                    title: {
                        display: true,
                        text: 'Products by Category'
                    },
                    datalabels: {
                        color: '#000',
                        anchor: 'end',
                        align: 'left',
                        font: {
                            size: 14
                        },
                        formatter: (value) => value
                    }
                }
            },
            plugins: [ChartDataLabels]
        });


        console.log(window.dashboardData.catLabels)
        console.log(window.dashboardData.catValues)
    }

    /* =====================
       CHART 2: Supplier Chart
       ===================== */
    const supplierCtx = document.getElementById("supplierChart");
    if (supplierCtx) {
        new Chart(supplierCtx, {
            type: 'bar',
            data: {
                labels: window.dashboardData.supLabels,
                datasets: [{
                    label: "Suppliers",
                    data: window.dashboardData.supValues
                }]
            },
            options: {
                
                indexAxis: 'y',
                // Elements options apply to all of the options unless overridden in a dataset
                // In this case, we are setting the border of each horizontal bar to be 2px wide
                elements: {
                    bar: {
                        borderWidth: 2,
                    }
                },
                responsive: true,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            usePointStyle: true
                        }
                    },
                    title: {
                        display: true,
                        text: 'Products by suppliers'
                    },
                    datalabels: {
                        color: '#000',  // Black is recommended for bars
                        anchor: 'end',
                        align: 'right',
                        font: {
                            //weight: 'bold',
                            size: 14,
                        },
                        formatter: (value) => value
                    }
                }
            },
            plugins: [ChartDataLabels]
        });


        console.log(window.dashboardData.supLabels)
        console.log(window.dashboardData.supValues)
    }

    /* ============================
       Modal - Products by Category
       ============================ */
    $(".modal-category").on("click", function () {
        let id = $(this).data("id");
        $.get("/Dashboard/GetProductsByCategory?id=" + id, function (data) {
            populateModalTable(data);
        });
        $("#detailsModal").modal("show");
    });

    /* ============================
       Modal - Products by Supplier
       ============================ */
    $(".modal-supplier").on("click", function () {
        let id = $(this).data("id");
        $.get("/Dashboard/GetProductsBySupplier?id=" + id, function (data) {
            populateModalTable(data);
        });
        $("#detailsModal").modal("show");
    });

    /* ============================
       Helper: Fill Modal Table
       ============================ */
    function populateModalTable(data) {
        let tbody = $("#modalTable tbody");
        tbody.html("");
        data.forEach(x => {
            tbody.append(`
                <tr>
                    <td><img src="${x.productImage}" height="60px"  </img></td>
                    <td>${x.productName}</td>
                    <td>${x.unitPrice}</td>
                    <td>${x.quantity}</td>
                </tr>
            `);
        });
    }

});
