var productsNames = [];
var products = [];

var SalesTotal = 0;
var taxTotal = 0;
var grandTotal = 0;
$(document).ready(function () {
    load();
});

function load() {
    getProductsNames();
    deleteRow();
    AddRow();
    selectProduct();
    submit();
    update();
    calculateAfterDiscount();
    getTotal();
    grandTotal -= $("#discount").val()
    $("#grandTotal span").text(grandTotal);
    $("#salesTotal span").text(SalesTotal);
    $("#taxTotal span").text(taxTotal);
}

function AddRow() {
    // Set Total
    getTotal();
    grandTotal -= $("#discount").val()
    $("#grandTotal span").text(grandTotal);
    $("#salesTotal span").text(SalesTotal);
    $("#taxTotal span").text(taxTotal);

    $("#addRow").click(function () {
        if ($('#data tr:last').children()[4].children[0].value != "") {
            var table = "";
            var c1 = `<td><select id="select" class="form-control mb-2" style="width:60%">
                <option>Please select one</option>
                ${productsNames}
            </select></td>`;

            var c2 = `<td><input id="NameAr" class="NameAr form-control" type="text" value="" disabled /></td>`;
            var c3 = `<td><input id="price" class="form-control" type="text" value="" /></td>`;
            var c4 = `<td><input id="Qty" class="form-control" value="" type="text"/></td>`;
            var c5 = `<td><input id="ItemTotal" class="form-control" value="" type="text" disabled /></td>`;
            var c6 = `<td><input id="tax" class="form-control" value="" type="text" /></td>`;
            var c7 = `<td><input id="TaxAmount" class="form-control" value="" type="text" disabled /></td>`;
            var c8 = `<td><input id="Total" class="form-control" value="" type="text" disabled /></td>`;
            var c9 = `<td><input id="Discount" class="form-control" value="" type="text"  /></td>`;
            var c10 = `<td><input id="DiscountAmount" class="form-control" value="" type="text" disabled /></td>`;
            var c11 = `<td><input id="NetTotal" class="form-control" value="" type="text" disabled /></td>`;

            var b = `<td><button id="deleteRow" class="delete btn btn-danger">X</button></td>`

            table = `<tr id="row">${c1}${c2}${c3}${c4}${c5}${c9}${c10}${c11}${c6}${c7}${c8}${b}</tr>`;
            $("#data").append(table);
        }
    });// end add

}

function getProductsNames() {
    $.ajax({
        type: "GET",
        url: "/Product/GetProductsNames",
        success: function (response) {
            console.log(response)
            $.each(response, function (i, item) {
                var option = `<option value="${item.value}">${item.text}</option>`;
                productsNames.push(option);
            });
        }

    });
}

function deleteRow() {
    $("#data").on("click", "#deleteRow", function () {
        $(this).closest("tr").remove();
        // Set Total
        getTotal();
        grandTotal -= $("#discount").val()
        $("#grandTotal span").text(grandTotal);
        $("#salesTotal span").text(SalesTotal);
        $("#taxTotal span").text(taxTotal);
    });
}

function selectProduct() {
    $("#data").on("change", "#select", function () {
        var row = $(this).closest("tr");
        var NameAr = row.children()[1].children[0];
        var unitPrice = row.children()[2].children[0];
        var quantity = row.children()[3].children[0];
        var salesTotal = row.children()[4].children[0];
        var discount = row.children()[5].children[0];
        var discountAmount = row.children()[6].children[0];
        var netTotal = row.children()[7].children[0];
        var tax = row.children()[8].children[0];
        var taxAmmount = row.children()[9].children[0];
        var total = row.children()[10].children[0];

        var selectedProduct = $(this).val();
        $.ajax({
            type: "GET",
            url: "/Product/GetProductByName",
            data: { Name: selectedProduct },
            success: function (response) {
                console.log(response);
                NameAr.value = response.productNameAr
                unitPrice.value = response.price
                quantity.value = 1
                salesTotal.value = unitPrice.value * quantity.value
                discount.value = 0
                discountAmount.value = 0
                netTotal.value = salesTotal.value
                tax.value = 0
                taxAmmount.value = 0
                total.value = netTotal.value

                $("#data").on("change", "#price", function () {
                    var currentTds = $(this).parent().nextAll();
                    var currentQty = currentTds[0].children[0];
                    var currentSalesTotal = currentTds[1].children[0];
                    var currentDiscount = currentTds[2].children[0];
                    var currentDiscountAmount = currentTds[3].children[0];
                    var currentNetTotal = currentTds[4].children[0];
                    var currentTax = currentTds[5].children[0];
                    var currentTaxAmmount = currentTds[6].children[0];
                    var currentTotal = currentTds[7].children[0];

                    currentSalesTotal.value = $(this).val() * currentQty.value
                    currentDiscountAmount.value = (parseFloat(currentDiscount.value) / 100) * currentSalesTotal.value
                    currentNetTotal.value = parseInt(currentSalesTotal.value) - parseInt(currentDiscountAmount.value)
                    currentTaxAmmount.value = Math.round(netTotal.value * parseFloat(currentTax.value / 100));
                    currentTotal.value = parseInt(netTotal.value) + parseInt(currentTaxAmmount.value);

                    // Set Total
                    getTotal();
                    grandTotal -= $("#discount").val()
                    $("#grandTotal span").text(grandTotal);
                    $("#salesTotal span").text(SalesTotal);
                    $("#taxTotal span").text(taxTotal);
                }); // end price

                $("#data").on("change", "#Qty", function () {
                    var currentNextTds = $(this).parent().nextAll();
                    var currentPrevTds = $(this).parent().prevAll();
                    var currentPrice = currentPrevTds[0].children[0];
                    var currentSalesTotal = currentNextTds[0].children[0];
                    var currentDiscount = currentNextTds[1].children[0];
                    var currentDiscountAmount = currentNextTds[2].children[0];
                    var currentNetTotal = currentNextTds[3].children[0];

                    var currentTax = currentNextTds[4].children[0];
                    var currentTaxAmmount = currentNextTds[5].children[0];
                    var currentTotal = currentNextTds[6].children[0];

                    currentSalesTotal.value = $(this).val() * currentPrice.value
                    currentDiscountAmount.value = currentSalesTotal.value * (currentDiscount.value/100)
                    currentNetTotal.value = parseInt(currentSalesTotal.value) - parseInt(currentDiscountAmount.value)
                    currentTaxAmmount.value = Math.round(currentNetTotal.value * parseFloat(currentTax.value / 100));
                    currentTotal.value = parseInt(currentNetTotal.value) + parseInt(currentTaxAmmount.value);

                    // Set Total
                    getTotal();
                    grandTotal -= $("#discount").val()
                    $("#grandTotal span").text(grandTotal);
                    $("#salesTotal span").text(SalesTotal);
                    $("#taxTotal span").text(taxTotal);
                }); // end quantity

                $("#data").on("change", "#Discount", function () {
                    var currentNextTds = $(this).parent().nextAll();
                    var currentPrevTds = $(this).parent().prevAll();
                    var currentPrice = currentPrevTds[2].children[0];
                    var currentQty = currentPrevTds[1].children[0];
                    var currentSalesTotal = currentPrevTds[0].children[0];
                    var currentDiscountAmount = currentNextTds[0].children[0];
                    var currentNetTotal = currentNextTds[1].children[0];
                    var currentTax = currentNextTds[2].children[0];
                    var currentTaxAmount = currentNextTds[3].children[0];
                    var currentTotal = currentNextTds[4].children[0];

                    currentDiscountAmount.value = parseFloat(parseFloat($(this).val()) / 100) * parseFloat(currentSalesTotal.value)
                    currentNetTotal.value = parseFloat(currentSalesTotal.value) - parseFloat(currentDiscountAmount.value)
                    currentTaxAmount.value = parseFloat(currentNetTotal.value) * (parseFloat(currentTax.value)/100);
                    currentTotal.value = parseInt(currentNetTotal.value) + parseInt(currentTaxAmount.value);

                    // Set Total
                    getTotal();
                    grandTotal -= $("#discount").val()
                    $("#grandTotal span").text(grandTotal);
                    $("#salesTotal span").text(SalesTotal);
                    $("#taxTotal span").text(taxTotal);
                }); // end discount

                $("#data").on("change", "#tax", function () {
                    var currentNextTds = $(this).parent().nextAll();
                    var currentPrevTds = $(this).parent().prevAll();
                    var currentPrice = currentPrevTds[5].children[0];
                    var currentQty = currentPrevTds[4].children[0];
                    var currentSalesTotal = currentPrevTds[3].children[0];
                    var currentDiscount = currentPrevTds[2].children[0];
                    var currentDiscountAmount = currentPrevTds[1].children[0];
                    var currentNetTotal = currentPrevTds[0].children[0];
                    var currentTaxAmmount = currentNextTds[0].children[0];
                    var currentTotal = currentNextTds[1].children[0];

                    currentTaxAmmount.value = Math.round(currentNetTotal.value * parseFloat($(this).val() / 100));
                    currentTotal.value = parseInt(currentNetTotal.value) + parseInt(currentTaxAmmount.value);
                    
                    // Set Total
                    getTotal();
                    grandTotal -= $("#discount").val()
                    $("#grandTotal span").text(grandTotal);
                    $("#salesTotal span").text(SalesTotal);
                    $("#taxTotal span").text(taxTotal);
                }); // end tax

                // Set Total
                getTotal();
                grandTotal -= $("#discount").val()
                $("#grandTotal span").text(grandTotal);
                $("#salesTotal span").text(SalesTotal);
                $("#taxTotal span").text(taxTotal);
            }
        });
    });
}
function getTotal() {
    products = [];
    SalesTotal = 0;
    taxTotal = 0;
    grandTotal = 0;
    $("#data tr").each(function (i, item) {
    console.log("s")
        if (item.id == "row") {
            if (isNaN(parseInt(item.children[4].children[0].value))) {
                SalesTotal += 0;
            } else {
                SalesTotal += parseInt(item.children[4].children[0].value);
                console.log(parseInt(item.children[4].children[0].value));
            }
            if (isNaN(parseInt(item.children[9].children[0].value))) {
                taxTotal += 0;
            } else {
                taxTotal += parseInt(item.children[9].children[0].value);
                parseInt(item.children[9].children[0].value)
            }
            if (isNaN(parseInt(item.children[10].children[0].value))) {
                grandTotal += 0;
            } else {
                grandTotal += parseInt(item.children[10].children[0].value);
                parseInt(item.children[10].children[0].value)
            }
            products.push({
                productName: item.children[0].firstElementChild.value,
                productNameAr: item.children[1].children[0].value,
                price: item.children[2].children[0].value,
                qty: item.children[3].children[0].value,
                Discount: item.children[5].children[0].value,
                tax: item.children[8].children[0].value,
                taxAmmount: taxTotal,
                salesTotal: SalesTotal,
                total: grandTotal,
            });
        }
    });
} //end total
function calculateAfterDiscount() {
    $("#discount").on("change", function () {
        getTotal();
        $("#salesTotal span").text(SalesTotal);
        grandTotal -= $(this).val();
        $("#grandTotal span").text(grandTotal);
        $("#taxTotal span").text(taxTotal);
    });

}

function submit() {
    $("#btnsubmit").click(function () {
        var date = new Date($("#ddlDate").val());
        var day = date.getDate();
        var month = parseInt(date.getMonth() + 1);
        var year = date.getFullYear();
        var mins = date.getMinutes();
        var hours = date.getHours();
        var sec = date.getSeconds();
        console.log(products, date);
        $.ajax({
            type: "POST",
            url: "/Invoice/Submit",
            data: {
                customerId: $("#ddlCustomerId").val(),
                Date: month + "/" + day + "/" + year,
                discount: $("#discount").val(),
                products: products
            },
            success: function (data) {
                console.log(data);
            }
        });
    })
}

function update() {
    $("#btnupdate").click(function () {
        $.ajax({
            type: "POST",
            url: "/Invoice/Update",
            data: {
                invID: $("#invID").val().split("-")[1],
                discount: $("#discount").val(),
                products: products
            },
            success: function (data) {
                console.log(data);
            }
        });
    })
}
$('table.table_data').each(function () {
    var currentPage = 0;
    var numPerPage = 5;
    var $table = $(this);
    $table.bind('repaginate', function () {
        $table.find('tbody tr').hide().slice(currentPage * numPerPage, (currentPage + 1) * numPerPage).show();
    });
    $table.trigger('repaginate');
    var numRows = $table.find('tbody tr').length;
    var numPages = Math.ceil(numRows / numPerPage);
    var $pager = $('<div class="pager"></div>');
    for (var page = 0; page < numPages; page++) {
        $('<span class="page-number"></span>').text(page + 1).bind('click', {
            newPage: page
        }, function (event) {
            currentPage = event.data['newPage'];
            $table.trigger('repaginate');
            $(this).addClass('active').siblings().removeClass('active');
        }).appendTo($pager).addClass('clickable');
    }

    $pager.insertAfter($table).find('span.page-number:first').addClass('active');


});