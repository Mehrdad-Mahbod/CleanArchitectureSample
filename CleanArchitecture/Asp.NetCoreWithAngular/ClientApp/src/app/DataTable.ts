import * as moment from 'jalali-moment';
/*mport { Directive, Injectable, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import * as MyPublicMethod from '../PublicMethod';*/



export class DataTable {
    Columns: any[] | undefined;
    ObjectList: any[] | undefined;

    //dtOptions: DataTables.Settings = {};
    dtOptions: any = {}; //Responsive 

    constructor(Columns: any[], ObjectList: any[]) {
        this.Columns = Columns;
        this.ObjectList = ObjectList;
    }

    GetOptionAngularDataTable(): any {
        this.dtOptions = {
            paging: true,
            jQueryUI: true, // ThemeRoller-stöd
            //bLengthChange: false,
            //filter: false,
            info: true,
            dom: 'lrftpi',
            autoWidth: true,
            scrollY: '30vh',
            //scrollCollapse: true,
            processing: true,
            pageLength: 5,
            data: this.ObjectList,
            destroy: true,
            lengthMenu: [[5, 10, 15, -1], ['پنج', 'ده', 'پانزده', "همه"]],
            columnDefs: [
                {"className": "dt-center", "targets": "_all"}
            ],
            columns: this.Columns,
            rowCallback: function (HtmlRow: any, Data: any, Index: any) {
            },
            drawCallback: function (settings: any) {
            },
            language: {
                url: "https://cdn.datatables.net/plug-ins/1.10.22/i18n/Persian.json",
                select: {
                    rows: {
                        _: " -- %d سطر انتخاب شده",
                        0: " -- برای انتخاب روی یک سطر کلیک کنید",
                        1: " -- انتخاب 1 سطر"
                    }
                }
            },
            //responsive:true
            responsive: {
                details: {
                    //responsive: true,
                    renderer: function (_api: any, _rowIdx: any, columns: any) {
                        var data = $.map(columns, function (col, i) {
                            let StrHtml;
                            if (col.title == "تاریخ ثبت") {
                                col.data = moment(col.data).format("jYYYY/jMM/jDD HH:mm:ss a");
                            }
                            if (col.title == 'حذف شده') {
                                StrHtml = '<tr data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '" class="CustomResponse">' +
                                    '<td>' + col.title + ':' + '</td> ';
                                //چون مقدار فیلد "حذف شده" را به صورت چک باکس در نظر گرفتیم لدا نمی توانیم
                                //مستقیما مقدار آن را بخوانیم لذا به روش زیر عمل می کنیم
                                let CheckboxStatusDeleted = $($(col.data)[0]).attr("Data-MyVal");
                                if (CheckboxStatusDeleted == "true") {
                                    StrHtml += '<td><input type=checkbox checked onclick="return false" /></td> ';
                                }
                                else {
                                    StrHtml += '<td><input type=checkbox onclick="return false" /></td> ';
                                }
                                StrHtml += '</tr>';
                            }
                            else {
                                StrHtml = '<tr data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '" class="CustomResponse">' +
                                    '<td>' + col.title + ':' + '</td> ' +
                                    '<td>' + col.data + '</td>' +
                                    '</tr>'
                            }
                            return col.hidden ? StrHtml : '';
                        }).join('');
                        return data ? $('<table/>').append(data) : false;
                    }
                }
            },
            select: true,
            //RowSelect: 'single',
            //dom: 'Rt', 
        };
        return this.dtOptions;
    }

    LoadJqueryFeture(MainTbl: any) {
        $(MainTbl).on('click', 'tbody tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });
        $(MainTbl).on('click', 'tbody tr td', function (e) {
            var Table = $('#MainTbl').DataTable();
            //var Table = FilesTbl.api();
            let RowIndex: number;

            var data = Table.row($(this).parents('tr')).data() as any;
            this.RowIndex = Table.row($(this).parents('tr')).index();

            if (Table.column($(this)).index() == 1) {
                //alert("دیتا= " + Table.cell(this).data() + " , سطر= " + Table.cell(this).index().row + " , ستون= " + Table.cell(this).index().columnVisible);
                var CurrentRowData = Table.row(this.RowIndex).data();
                //console.log(CurrentRowData);
            }
            if (Table.column($(this)).index() == 2) {
                //alert("دیتا= " + Table.cell(this).data() + " , سطر= " + Table.cell(this).index().row + " , ستون= " + Table.cell(this).index().columnVisible);
                var CurrentRowData = Table.row(this.RowIndex).data();
                //console.log(CurrentRowData);
            }

        });
        //برای کنترل باز و بسته کردن سطر های ریسپانسیو شده
        $(MainTbl).on('click', 'tbody tr td.RowDetails', function (e) {
            var tr = $(this).closest('tr');
            var td = $(this).closest('td');
            var row = $("#MainTbl").DataTable().row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                //row.child.hide();
                tr.removeClass('shown');
                td.removeClass('shown');
                //console.log($(tr[0].cells.item(5))[0].outerText);            
                $(".dtr-data").on("click", function () {
                    //console.log(this);
                });
            }
            else {
                // Open this row
                tr.addClass('shown');
                td.addClass('shown');
            }
        });
        //این قسمت درست کار می کند
        $(MainTbl).on("click", "tbody input[type='checkbox'].SelectRow", function (e) {
            let ClosestRow = $(this).closest('tr');
            console.log($("#MainTbl").DataTable());
            let Data = $("#MainTbl").DataTable().rows(ClosestRow).data() as any;
            //var RowNode = $("#MainTbl").DataTable().row(ClosestRow).node();
            let MainData: any;
            if (Data[0] === undefined) {
                let SelectedRow = $(this).parents('tr');
                if (SelectedRow.hasClass('child')) {
                    SelectedRow = SelectedRow.prev();
                }
                Data = $('#MainTbl').DataTable().row(SelectedRow).data() as any;
                $(ClosestRow).addClass('selected');
                MainData = Data;
            }
            else {
                MainData = Data[0];
            }
            if (this.checked) {
                console.log(this.checked + "**" + JSON.stringify(MainData));
                /*ReceiverletterList.push(
                    {
                        "ID": 0,
                        "LettersId": 1,
                        "UserId": Data[0].Id,
                    });*/
            }
            else {
                console.log(this.checked + "**" + JSON.stringify(MainData));
                /*$(ClosestRow).removeClass('selected');
                for (var i = 0; i < ReceiverletterList.length; i++) {
                    //alert(ReceiverletterList.ID);
                    if (ReceiverletterList[i].ReceiverUserId === Data[0].Id) {
                        ReceiverletterList.splice(i, 1);
                    }
                }*/
            }
        });
        //Checkbox حلقه برای  
        //$("#MainTbl input[type=checkbox].SelectRow/*:checked*/").each(function (Index, RValue) {
        // var aData = TblReceivers.fnGetData(Index);
        //if (aData.Id === Value.ReceiverUserId) {
        //  $(RValue).prop('checked', 1);
        // if (Value.IsSelected == true) {
        //       $(this).prop('checked', true);
        //  }
        //}
        //});
    }
}
