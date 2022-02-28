import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import Swal from 'sweetalert2';
import { DataTableDirective } from 'angular-datatables';
import * as moment from 'jalali-moment';

import { ToastrService } from 'ngx-toastr';
import { PublichttpService } from '../Services/publichttp.service';
import { ServerurlService } from '../Services/serverurl.service';

import { DataTable } from '../DataTable';
import { GetCurrentUser } from '../GetCurrentUser';

import { GeneralOfficeVM } from '../ViewModels/GeneralOfficeVM';


@Component({
  selector: 'app-generaloffice',
  templateUrl: './generaloffice.component.html',
  styleUrls: ['./generaloffice.component.css']
})
export class GeneralofficeComponent implements OnInit {

  constructor(private HttpService: PublichttpService, private ServerUrl: ServerurlService, private toastr: ToastrService) { }

  @ViewChild('dataTable', { static: true }) table!: ElementRef;
  @ViewChild(DataTableDirective) datatableElement: DataTableDirective | undefined;
  @ViewChild('Name') Name: ElementRef<HTMLInputElement> | undefined;
  //@ViewChild('Priority') Priority: ElementRef<HTMLInputElement> | undefined;
  //dtOptions: DataTables.Settings = {};  //Not Responsive 
  dtOptions: any = {}; //Responsive 
  public dataTable: any;
  dtTrigger: Subject<any> = new Subject<any>();
  GeneralOfficeVMList: GeneralOfficeVM[] = [];
  public InputForm!: FormGroup;
  public BtnStateRegister: boolean = false;
  public BtnStateDelete: boolean = false;
  public EditCommand: boolean = false;

  ngOnInit(): void {
    let MySelf = this;
    this.GetAll();
    //MyPublicMethod.OnlyNumber();
    /*نباشد Null دستی یک مقدار اولیه تنظیم می کنیم تا شی مورد نظر*/
    /*IsDeleted مثلا برای */
    this.InputForm = new FormGroup({
      'ID': new FormControl(0),
      'Name': new FormControl(null, [Validators.required]),
      'Priority': new FormControl(null, [Validators.required, Validators.pattern("(0?[1-9]|[1-9][0-9]|[1][1-9][1-9]|200)")]),
      'IsDeleted': new FormControl(false)
    });


    let Columns = [
      {   // Responsive control column
        title: 'جزئیات',
        data: null,
        defaultContent: '',/*<i class="fa fa-plus-square"></i>*/
        className: 'control RowDetails',
        orderable: false,
        visible: true,
      },
      { title: 'کد اصلی', data: 'id' },
      //{ title: 'کد کاربر', data: 'userId' },
      { title: 'نام گروه', data: 'name' },
      { title: 'اولویت', data: 'priority' },
      { title: 'تاریخ ثبت', data: 'addedDate' },
      {
        title: 'حذف شده',
        data: 'isDeleted',
        visible: true,
        render: function (data: any, type: any, row: any) {
          if (type === 'display') {
            return '<input type="checkbox"  /*checked*/ class="IsDeleted" Data-MyVal=' + data + ' onclick="return false;" >';
            //return '<input type="checkbox" class="editor-active" name="id[]" ">';
          }
          return data;
        },
      },
      {
        title: 'انتخاب',
        data: null,
        visible: true,
        render: function (data: any, type: any, row: any) {
          if (type === 'display') {
            return '<input type="checkbox"  /*checked class="SelectRow"  >';
          }
          return data;
        },
      }
    ];
    let Dt = new DataTable(Columns, this.GeneralOfficeVMList);
    this.dtOptions = Dt.GetOptionAngularDataTable();
    Dt.LoadJqueryFeture($("#MainTbl"));

    this.dtOptions.rowCallback = function (HtmlRow: any, Data: any, Index: any) {
      //console.log(Index + "***" + JSON.stringify(Data));        
      const self = this;
      if ( /*(Data as any).IsActive OR */ (<any>Data).isDeleted == true) {
        $('.IsDeleted', HtmlRow).prop('checked', (Data as any).isDeleted == 1);
      }
      if ((Data as any).addedDate) {
        //alert(moment((Data as any).addedDate).format("jYYYY/jMM/jDD"));
        $('td:eq(4)', HtmlRow).html(moment((Data as any).addedDate).format("jYYYY/jMM/jDD HH:mm:ss a"));
      }
      $('td', HtmlRow).off('click');

      $('td', HtmlRow).on('click', () => {
        if (MySelf.EditCommand == false) {
          //به همین شکل استفاده کرد patchValue می توان از  reset به جای
          MySelf.InputForm.reset({
            'ID': (Data as any).id,
            'Name': (Data as any).name,
            'Priority': (Data as any).priority,
            'IsDeleted': (Data as any).isDeleted /*false*/
          });
          MySelf.Name?.nativeElement.focus();
        }
      });

      /*$('td', HtmlRow).on('dblclick', () => {
        alert("123");
      })*/


    }
    this.dtOptions.drawCallback = function (settings: any) {
      //console.log(settings);
      var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
      //console.log(pagination);
      //pagination.toggle(settings.oApi.page.info()!.pages > 1);
    }

    $("document").on("ready", function () {

    });

    $("#MainModal").on('hidden.bs.modal', function () {
      MySelf.BtnNew();
    });

  }


  HideModal() {
    //(<any>$("#MainModal")).modal("toggle");
    //(<any>$("#MainModal")).modal({backdrop: false});
    (<any>$("#MainModal")).modal("hide");
  }

  BtnNew() {
    this.BtnStateRegister = false;
    this.EditCommand = false;
    this.BtnStateDelete = false;

    //alert((<HTMLInputElement>document.getElementById("Priority")).value);

    this.GetControl['Name'].setValue('');
    this.GetControl['Priority'].setValue('');

    this.Name!.nativeElement.focus();


    this.InputForm.controls['Name'].reset();
    this.InputForm.controls['Priority'].reset();


    $("#EditIcon").removeClass();
    $("#EditIcon").addClass("fa fa-pencil-square-o fa-lg");
  }

  BtnReloadDataTable() {
    this.GetAll();
  }

  BtnRegister() {
    this.BtnNew();
  }

  BtnUpdate() {
    if (this.InputForm.status != "INVALID") {
      this.EditCommand = true;
      this.BtnStateDelete = false;
      this.Name?.nativeElement.focus();
      $("#EditIcon").removeClass();
      $("#EditIcon").addClass("spinner-grow spinner-grow-sm text-danger");
    }
    else {
      this.toastr.warning('اطلاعاتی برای ویرایش انتخاب نکرده اید', 'خطا');
    }

  }

  BtnDeleted() {
    /*
    if (this.EditCommand == true) {
    }
    else {      
      this.toastr.warning('قبل از حذف اطلاعات کلید ویرایش را فعال نمایید!');
    }
    */
    if (this.InputForm.valid) {
      let CurrentUser = new GetCurrentUser();

      let GeneralOfficeVM: GeneralOfficeVM = Object.assign({}, this.InputForm.value);
      //ExaminationVM.userId = Number.parseInt(CurrentUser!.GetUserID());

      Swal.fire({
        title: 'توجه',
        text: 'آیا تمایلی به حذف اطلاعات دارید؟?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'بلی',
        cancelButtonText: 'خیر'
      }).then((result: any) => {
        if (result.value) {
          this.HttpService.HttpPost(this.ServerUrl.Url + "/GeneralOffice/RemoveGeneralOffice", GeneralOfficeVM).subscribe(
            Data => {
              //console.log(Data);
              Swal.fire(
                'حذف شد!',
                'اطلاعات با موفقیت از سیستم حذف شد',
                'success'
              ).then((result) => {
                this.BtnNew();
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                } else if (result.isDenied) {
                }
              })
            },
            (error) => {
              console.log(error);
              alert(JSON.stringify(error));
            },
            () => {
              //this.toastr.success('با موفقیت حذف شد.', 'تایید');
              this.GetAll();
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          Swal.fire('انصراف', 'اطلاعاتی حذف نشد', 'error');
          this.BtnNew();
        }
      })
    }
    else {
      this.toastr.warning('اطلاعاتی برای حذف وجود ندارد.لطفا یک سطر را برای حذف انتخاب نمایید!', 'توجه');
    }
  }

  get GetControl() {
    return this.InputForm.controls;
  }

  ngAfterViewInit(): void {
  }

  /*برگرفته از این سایت*/
  //https://medium.com/ramsatt/integrate-data-table-with-angular-8-application-with-json-backend-f1071feeb18f
  GetAll() {
    let CurrentUser = new GetCurrentUser();

    let GeneralOfficeVM = {} as GeneralOfficeVM;

    //GeneralOfficeVM.userId = Number.parseInt(CurrentUser!.GetUserID());


    this.HttpService.HttpGetWithObject(this.ServerUrl.Url + "/GeneralOffice/FetchListGeneralOffice", GeneralOfficeVM).subscribe(
      Data => {
        this.GeneralOfficeVMList = Data;
        //this.ExaminationList = (Data as any).data;
        this.dtOptions.data = this.GeneralOfficeVMList;
      },
      (error) => {
        alert(JSON.stringify(error));
      },
      () => {
        this.dataTable = $(this.table.nativeElement);
        this.dataTable.DataTable(this.dtOptions);
      });
  }

  PostData() {
    this.BtnStateRegister = true;
    let CurrentUser = new GetCurrentUser();

    let GeneralOfficeVM: GeneralOfficeVM = Object.assign({}, this.InputForm.value);
    //let GeneralOfficeVM = {} as GeneralOfficeVM;
    //GeneralOfficeVM.userId = Number.parseInt(CurrentUser!.GetUserID());

    if (this.InputForm.valid) {
      if (this.EditCommand == false) { //Save (EditCommand Is false)
        Swal.fire({
          title: 'توجه',
          text: 'آیا تمایلی به ثبت اطلاعات دارید؟',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'بلی',
          cancelButtonText: 'خیر'
        }).then((result: any) => {
          if (result.isConfirmed) {
            GeneralOfficeVM.id = 0;
            this.HttpService.HttpPost(this.ServerUrl.Url + "/GeneralOffice/RegisterGeneralOffice", GeneralOfficeVM).subscribe(
              Data => {
                //console.log(Data);
                this.toastr.success('با موفقیت ثبت شد.', 'تایید');
              },
              (Error) => {
                console.log(Error);
                alert(JSON.stringify(Error.error.Message));
                this.BtnStateRegister = false;
              },
              () => {
                this.BtnNew();
                this.HideModal();
                this.GetAll();
              });
          } else {
            Swal.fire('انصراف', 'اطلاعاتی ثبت نشد', 'error')
              .then((result) => {
                if (result.isConfirmed) {
                  this.BtnNew();
                  this.HideModal();
                }
              });
          }
        });
      }
      else { //Edit  (EditCommand Is true) 
        this.HttpService.HttpPost(this.ServerUrl.Url + "/GeneralOffice/EditGeneralOffice", GeneralOfficeVM).subscribe(
          Data => {
            this.toastr.success('به روز رسانی انجام شد!', 'تایید');
          },
          (Error) => {
            console.log(Error);
            alert(JSON.stringify(Error.error.Message));
          },
          () => {
            this.BtnNew();
            this.HideModal();
            this.GetAll();
          });
      }
    }
    else {
      this.toastr.error('اطلاعات وارد صحیح نیست.لطفا مجددا بررسی نمایید!', 'خطا');
      this.BtnStateRegister = false;
      this.BtnNew();
      this.HideModal();
    }
  }

}
