
function loadTransaction() {
    $.ajax({
        url: "/Transaction/GetList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            generateGrid(result);
        },
        error: function (errormessage) {
            $(".container").html(errormessage);
            /*alert(errormessage.responseText);*/
        }
    });
}

function insertTransaction(transaction, handleSuccess) {
    $.ajax({
        url: "/Transaction/InsertTransaction",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(transaction),
        success: function result(result) {
            loadTransaction();
            console.log(result); handleSuccess();
        },
        error: function error(error) {
            alert(error.responseText);
        }
    });
    return "";
}

function updateTransaction(transaction) {
    $.ajax({
        url: "/Transaction/UpdateTransaction",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(transaction),
        success: function result(result) {
            loadTransaction();
            console.log(result);
        },
        error: function error(error) {
            alert(error.responseText);
        }
    });
}
function deleteTransaction(transaction) {
    $.ajax({
        url: "/Transaction/DeleteTransaction/" + transaction.TransactionID,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(transaction),
        success: function result(result) {
            loadTransaction();
            console.log(result);
        },
        error: function error(error) {
            alert(error.responseText);
        }
    })
}

function generateGrid(response) {
    $("#transaction-list").kendoGrid({
        dataSource: {
            transport: {
                read: function (options) {
                    options.success(response.data);
                }
            }
        },
        pageSize: 5,
        columns: [
            {
                field: "BankName",
                title: "Bank Name"
            },
            {
                field: "AccountNumber",
                title: "Account Number"
            },
            {
                field: "BeneficiaryName",
                title: "Beneficiary Name"
            },
            {
                field: "SWIFTCode",
                title: "SWIFT Code"
            },
            {
                field: "Amount",
                title: "Amount"
            },
            {
                field: "Date",
                title: "Date",
                template: "#= kendo.toString(kendo.parseDate(Date, 'yyyy-MM-dd'), 'dd/MM/yyyy') #"
            },
            {
                title: "Action",
                command: [
                    {
                        name: "edit",
                        iconClass: "k-icon k-i-copy",
                        click: function (e) {
                            e.preventDefault();
                            let dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                            let { AccountNumber, BankName, BeneficiaryName, Amount, SWIFTCode, Date } = dataItem;
                            updateWindowProps({ AccountNumber, BankName, BeneficiaryName, Amount, SWIFTCode, Date }, true);
                            /*                            $("#form").data("kendoForm").setOptions({ formData: { AccountNumber, BankName, BeneficiaryName, Amount, SWIFTCode } });*/
                            /*form.editable.options.model.set("BeneficiaryName", dataItem["BeneficiaryName"]);*/
                            $("#window").data("kendoWindow").open();
                        }
                    },
                    {
                        name: "delete",
                        iconClass: "k-icon k-i-delete",
                        click: function (e) {
                            e.preventDefault();
                            let dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                            let { TransactionID } = dataItem;
                            deleteTransaction({ TransactionID });
                        }
                    }
                ]
            }
        ],
        pageable: true,
        editable: false
    });
}
function buildTransactinForm(isUpdate) {
    $("#form").kendoForm({
        orientation: "vertical",
        formData: {
            AccountNumber: "",
            BeneficiaryName: "",
            BankName: "",
            SWIFTCode: "",
            Amount: 0,
            Date: new Date()
        },
        items: [{
            type: "group",
            label: "Transaction Details",
            items: [
                {
                    field: "AccountNumber",
                    label: "Account Number:",
                    validation: { required: true, }
                },
                {
                    field: "BeneficiaryName",
                    label: "Beneficiary Name:",
                    validation: { required: true }
                },
                //{
                //    field: "Password",
                //    label: "Password:",
                //    validation: { required: true },
                //    hint: "Hint: enter alphanumeric characters only.",
                //    editor: function (container, options) {
                //        container.append($("<input type='password' class='k-textbox k-valid' id='Password' name='Password' title='Password' required='required' autocomplete='off' aria-labelledby='Password-form-label' data-bind='value:Password' aria-describedby='Password-form-hint'>"));
                //    }
                //},
                { field: "BankName", label: "Bank Name:", validation: { required: true } },
                { field: "SWIFTCode", label: { text: "SWIFT Code:", optional: true } },
                { field: "Amount", label: "Amount:", validation: { required: true } },
                /*{ field: "Agree", label: "Agree to Terms:", validation: { required: true } }*/
            ]
        }],
        validateField: function (e) {

        },
        submit: function (e) {
            e.preventDefault();
            insertTransaction(e.model);
        },
        clear: function (ev) {

        },
    });
}

function updateWindowProps(model, isUpdate) {
    let form = $("#form").data("kendoForm");
    if (isUpdate) {
        form.setOptions({
            formData: model,
            submit: function myfunction(e) {
                e.preventDefault();
                updateTransaction(e.model);
            }
        });
    } else {
        form.clear();
        form.setOptions({
            formData: {
                AccountNumber: "",
                BeneficiaryName: "",
                BankName: "",
                SWIFTCode: "",
                Amount: 0,
                Date: new Date()
            },
            submit: function myfunction(e) {
                e.preventDefault();
                insertTransaction(e.model);
            }
        });
    }
}