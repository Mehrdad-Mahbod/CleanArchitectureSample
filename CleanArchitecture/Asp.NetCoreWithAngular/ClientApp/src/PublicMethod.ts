import * as $ from "jquery";
/*declare var $: any;*/

export function MainFaunction() {
  $("#MyCollapse li").children('ul').hide();

  //$("#BtnMainMenu").click(function (e) {
  $("#BtnMainMenu").on('click', function (e) {
    //e.preventDefault();
    $("#wrapper").toggleClass("DivWrapper");
    $('#menu ul').hide();
    if ($('.navbar-collapse.collapse.show').hasClass('show')) {
      $('.navbar-collapse.collapse.show').removeClass('show');
      $("#wrapper").toggleClass("toggled");
    }
  });

  /*منوهای ثابت بالای صفحه*/
  //$('#menu li a').click(function (/*e*/)  {
  $('#menu li a').on('click', function (): void {
    console.log('Static Menu');
    var checkElement = $(this).next();
    if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
      $('#menu ul:visible').slideUp('normal');
      //return false;
    }
    if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
      $('#menu ul:visible').slideUp('normal');
      checkElement.slideDown('normal');
      //return false;
    }
    /*سایدبار را می بندد*/
    if (!$("#wrapper").hasClass('DivWrapper')) {
      $("#wrapper").addClass("DivWrapper");
    }
    /*برای وقتی هست صفحه به حالت کولاپس قرار دارد*/
    if ($("#wrapper").hasClass('toggled')) {
      $("#wrapper").removeClass("toggled");
    }
    $("#MyCollapse li").children('ul').hide(200);
  });


  var bsDefaults = {
    offset: false,
    overlay: false,
    /*width: '330px'*/
    width: '250px'
  },
    bsMain = $('.bs-offset-main'), bsOverlay = $('.bs-canvas-overlay');
  $('[data-toggle="canvas"][aria-expanded="false"]').on('click', function () {
    var canvas = $(this).data('target'),
      opts = $.extend({}, bsDefaults, $(canvas).data()),
      prop = $(canvas).hasClass('bs-canvas-right') ? 'margin-right' : 'margin-left';

    if (opts.width === '100%')
      opts.offset = false;

    $(canvas).css('width', opts.width);
    if (opts.offset && bsMain.length)
      bsMain.css(prop, opts.width);

    $(canvas + ' .bs-canvas-close').attr('aria-expanded', "true");
    $('[data-toggle="canvas"][data-target="' + canvas + '"]').attr('aria-expanded', "true");
    if (opts.overlay && bsOverlay.length)
      bsOverlay.addClass('show');
    /*Add By Mehrdad*/
    if ($(canvas).width() == 250 && <any>$(window).width() < 768) {
      bsOverlay.removeClass('show');
      $(canvas).css('width', 0);
    }
    if ($(canvas).width() == 250 && <any>$(window).width() > 768) {
      bsOverlay.removeClass('show');
      $(canvas).css('width', 50);
    }
    if ($(canvas).width() == 0 && <any>$(window).width() < 768) {
      bsOverlay.removeClass('show');
      $(canvas).css('width', 250);

    }
    /*End By Mehrdad*/

    return false;
  });
  $('.bs-canvas-close, .bs-canvas-overlay').on('click', function () {
    var canvas, aria;
    if ($(this).hasClass('bs-canvas-close')) {
      canvas = $(this).closest('.bs-canvas');
      aria = $(this).add($('[data-toggle="canvas"][data-target="#' + canvas.attr('id') + '"]'));
      if (bsMain.length)
        bsMain.css(($(canvas).hasClass('bs-canvas-right') ? 'margin-right' : 'margin-left'), '');
    } else {
      canvas = $('.bs-canvas');
      aria = $('.bs-canvas-close, [data-toggle="canvas"]');
      if (bsMain.length)
        bsMain.css({
          'margin-left': '',
          'margin-right': ''
        });
    }
    if (<any>$(window).width() < 768) {
      $("#bs-canvas-right").css('width', '0');
    }
    else {
      canvas.css('width', '50');
      //canvas.css('width', '0');
    }
    aria.attr('aria-expanded', "false");
    //if (bsOverlay.length)
    bsOverlay.removeClass('show');
    return false;
  });
  $('#SidebarWrapper,#bs-canvas-right').hover(function () {
    var canvas = $('#bs-canvas-right'),
      opts = $.extend({}, bsDefaults, $(canvas).data()),
      prop = $(canvas).hasClass('bs-canvas-right') ? 'margin-right' : 'margin-left';

    if (opts.width === '100%')
      opts.offset = false;

    $(canvas).css('width', opts.width);
    if (opts.offset && bsMain.length)
      bsMain.css(prop, opts.width);

    if (opts.overlay && bsOverlay.length)
      bsOverlay.addClass('show');

    return false;
  }, function () {
    var canvas, aria;
    if ($(this).hasClass('bs-canvas-close')) {
      canvas = $(this).closest('.bs-canvas');
      aria = $(this).add($('[data-toggle="canvas"][data-target="#' + canvas.attr('id') + '"]'));
      if (bsMain.length)
        bsMain.css(($(canvas).hasClass('bs-canvas-right') ? 'margin-right' : 'margin-left'), '');
    } else {
      canvas = $('.bs-canvas');
      aria = $('.bs-canvas-close, [data-toggle="canvas"]');
      if (bsMain.length)
        bsMain.css({
          'margin-left': '',
          'margin-right': ''
        });
      //Close All Menu After Live Side
      $('#StaticMenu ul,#DynamicMenu ul').hide(500);
      $('#StaticMenu ul,#DynamicMenu ul').children('.current').parent().show(500);
      $("#MyCollapse li").children('ul').hide(500);        
    }
    canvas.css('width', '50');
    //canvas.css('width', '0');
    aria.attr('aria-expanded', "false");
    if (bsOverlay.length)
      bsOverlay.removeClass('show');

    return false;
  });
  $(window).on("resize", function () {
    if (<any>$(window).width() < 768) {
      //$("#bs-canvas-right").css('width', '0');
      //$("#MainRenderBody").removeClass("ms-5");

    }
    else {
      //$("#bs-canvas-right").css('width', '50');
      //$("#MainRenderBody").addClass("ms-5");
    }
  });

  $('#StaticMenu ul,#DynamicMenu ul').hide();
  $('#StaticMenu ul,#DynamicMenu ul').children('.current').parent().show();
  //$('#menu ul:first').show();
  $('#StaticMenu li a,#DynamicMenu li a').click(function () {
    //e.preventDefault();

    var TimeEfect = 500;
    var checkElement = $(this).next();
    //console.log($(checkElement));
    if (checkElement.is(':visible')) {
      console.log("Close");
      checkElement.slideUp(TimeEfect);//.animate();
      checkElement.find('ul:visible').slideUp(TimeEfect);//.animate();
      /*if ($(this).children().hasClass('fa fa-minus-circle')) {
          $(this).children().removeClass('fa fa-minus-circle');
          $(this).children().addClass('fa fa-plus-circle');
          $(this).addClass('fa fa-plus-circle');
      }*/
      (<any>$(this).find("i:last")).switchClass("fa-angle-down", "fa-angle-left", 2, "easeInOutQuad");
    }
    else if (!checkElement.is(':visible')) {
      console.log("Open");
      (<any>$(this).find("i:last")).switchClass("fa-angle-left", "fa-angle-down", 2, "easeInOutQuad");

      var Parent = $(this).parents('ul').first();
      Parent.find('ul:visible').slideUp(TimeEfect);//.animate();
      checkElement.slideDown(TimeEfect);//.animate();
      $('#SidebarWrapper').animate({ scrollTop: $(this).offset()?.top }, 900);
      /*if ($(this).children().hasClass('fa fa-plus-circle')) {
          $(this).children().removeClass('fa fa-plus-circle');
          $(this).children().addClass('fa fa-minus-circle');
          $(this).addClass('fa fa-plus-circle');
      }*/
      /*برای پاک کردن بردر حالت تودرتو*/
      $("li ul li").css('border', '0');
      if (checkElement.length == 0) {//بستن اسلایدر
        $("#bs-canvas-right").css('width', '50');
        $("#StaticMenu li,#DynamicMenu li").children('ul').hide(200);
      }
    }
    //e.stopPropagation();
  });
}

export function MyCollapsechildrenHide() {
  $("#MyCollapse li").children('ul').hide();
}

/*منوهای دیتابیس*/
export function OpenMenuDataBase() {
  $('#MyCollapse li a').on('click', function (e) {
    e.preventDefault();
    var TimeEfect = 500;
    var checkElement = $(this).next();
    if (checkElement.is(':visible')) {
      checkElement.slideUp(TimeEfect);//.animate();
      checkElement.find('ul:visible').slideUp(TimeEfect);//.animate();
      /*if ($(this).children().hasClass('fa fa-minus-circle')) {
          $(this).children().removeClass('fa fa-minus-circle');
          $(this).children().addClass('fa fa-plus-circle');
          $(this).addClass('fa fa-plus-circle');
      }*/
    }
    else if (!checkElement.is(':visible')) {
      var Parent = $(this).parents('ul').first();
      Parent.find('ul:visible').slideUp(TimeEfect);//.animate();
      checkElement.slideDown(TimeEfect);//.animate();
      $('#SidebarWrapper').animate({ scrollTop: $(this).offset()?.top }, 900);

      /*if ($(this).children().hasClass('fa fa-plus-circle')) {
          $(this).children().removeClass('fa fa-plus-circle');
          $(this).children().addClass('fa fa-minus-circle');
          $(this).addClass('fa fa-plus-circle');
      }*/
      /*برای پاک کردن بردر حالت تودرتو*/
      $("li ul li").css('border', '0');
    }
    e.stopPropagation();
  });
}

export function MoveToNextControlWithEnter() {
  $.extend($.expr[':'], {
    focusable: function (el: any, index: any, selector: any) {
      return $(el).is('a, button, :input, [tabindex],:radio');
    }
  });

  $(document).on('keydown', ':focusable', function (e) {
    if (e.which == 13) {
      e.preventDefault();
      // Get all focusable elements on the page
      var $canfocus = $(':focusable');
      var index = $canfocus.index(this) + 1;
      if (index >= $canfocus.length) index = 0;
      $canfocus.eq(index).focus();
    }
  });
}

export function ValidateMobileNumber(MobileNumber: string) {
  var Mobile = MobileNumber;
  var pattern = /^\d{11}$/;
  if (pattern.test(Mobile)) {
    return true;
  }
  else {
    return false;
  }
}

export function OnlyNumber() {
  $('input.Number').keyup(function (event) {
    // skip for arrow keys
    if (event.which >= 37 && event.which <= 40) {
      event.preventDefault();
    }
    $(this).val(function (index, value) {
      return value.replace(/\D/g, '');
    });
  });
}
export function OnlyNumberWithSeparatingThreeDigits() {
  $('input.NumberWithSeparatingThreeDigits').keyup(function (event) {
    // skip for arrow keys
    if (event.which >= 37 && event.which <= 40) {
      event.preventDefault();
    }
    $(this).val(function (index, value) {
      return value
        .replace(/\D/g, '')
        .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
        ;
    });
  });
}

export function RemoveComma(str: string) {
  var myString = '',
    chrCode;
  for (var i = String(str).length - 1; i >= 0; --i) {
    chrCode = String(str).charCodeAt(i);
    if (chrCode != 44) {
      myString = String.fromCharCode(chrCode) + myString;
    }
  }
  return String(myString);
  /*return String(str).replace(/,/g, "");*/
}

export function CreateNestedData(data: any) {
  var Source = [];
  //var Items = [];
  var Items: any[] = [];

  // build hierarchical source.
  for (var i = 0; i < data.length; i++) {
    var Item = data[i];
    var ID = Item["id"];
    var UserRolesID = Item["UserRolesID"];
    var ParentId = Item["parentId"];
    var Name = Item["name"];
    var Component = Item["component"];
    var Icon = Item["icon"];
    if (Items[ParentId]) {
      let Item;
      Item = { ID: ID, UserRolesID: UserRolesID, ParentID: ParentId, Name: Name, Component: Component, Icon: Icon, Item: Item };
      if (!Items[ParentId].Items) {
        Items[ParentId].Items = [];
      }
      Items[ParentId].Items[Items[ParentId].Items.length] = Item;
      Items[ID] = Item;
    }
    else {
      if (ID == ParentId) {
        Items[ID] = { ID: ID, UserRolesID: UserRolesID, ParentID: ParentId, Name: Name, Component: Component, Icon: Icon/*, Item: Item */ };
      }
      else {
        Items[ID] = { ID: ID, UserRolesID: UserRolesID, ParentID: ParentId, Name: Name, Component: Component, Icon: Icon, Item: Item };
      }
      Source[ID] = Items[ID];
    }
  }
  return Source;
}

export var CreateUL = function (Ul: any, Items: any) {
  /*
    $.each(Items, function (I, J) {
      console.log(Items);
      if (J != null) {
  
  
        let li = $("<li style='list-style: none;' >" +
          "<a class='Link' href='JavaScript:void(0)' data-url='" + this.Component + "' data-ParentId='" + this.ParentId + "' >" +
          "<b class='fa fa-chevron-circle-down' style='width:auto;float: right;line-height:unset;'></b> " + " " +
          "<b class='" + this.Icon + "' style='width:auto;float: left;line-height:unset;'></b> " + " " +
          " " + this.Name + " " +
          //"<span class='badge pull-left'>0</span>" +
          "</a>" +
          "</li>");
        li.appendTo(Ul);
        if (this.Items && this.Items.length > 0) {
          var ul = $("<ul></ul>");
          ul.css({ 'width': 'auto', 'margin-right': '5px', 'margin-left': '-30px', 'direction': 'ltr' });
          ul.appendTo(li);
          //$("li>ul>li>a.Link").css({ 'width': '240px', 'float': 'right' });
          CreateUL(ul, this.Items);
        }
      }
    });
    */
  /*
کد قدیمی که درست کار می کرد
$.each(Items, function () {
    if (this.Name) {
        li = $("<li style='list-style: none;' >" +
            "<a class='Link' href='javascript:void(0)' data-url='" + this.Controller + '/' + this.Action + "' data-ParentId='" + this.ParentID + "' >" +
            "<b class='" + this.Icon + "' style='width:auto;margin:0 0 0 1%'></b> " + " " +
            " " + this.Name + " " +
            "<span class='badge pull-left'>0</span>" +
            "</a>" +
            "</li>");
        li.appendTo(parent);
        if (this.Items && this.Items.length > 0) {
            var ul = $("<ul></ul>");
            ul.appendTo(li);
            CreateUL(ul, this.Items);
        }
    }
});
خودم نوشتم
*/

  $.each(Items, function (I, J) {
    if (J != null) {
      //<li>
      //    <a href="#"><i class="fa fa-cart-plus"></i><span class="ms-5">Events</span></a>
      //</li>
      //li = $("<li style='list-style: none;' >" +
      //    "<a class='Link' href='javascript:void(0)' data-url='" + this.Controller + '/' + this.Action + "' data-ParentId='" + this.ParentID + "' >" +
      //    "<b class='" + this.Icon + "' style='width:auto;margin:0 0 0 1%'></b> " + " " +
      //    " " + this.Name + " " +
      //    /*"<span class='badge pull-left'>0</span>" +*/
      //    "</a>" +
      //    "</li>");
      let li;
      if (this.ParentID == null) {
        li = $("<li style='list-style: none;' class='rounded /*bg-dark*/ /*bg-gradient*/ text-white'>" +
          "<a class='Link' href='javascript:void(0)' data-url='" + this.Component + "' data-ParentId='" + this.ParentID + "' >" +
          "<i class='" + this.Icon + "'></i><span class=ms-2>" + this.Name + "</span> " +
          "<div class='pull-left me-2'>"+
          "<i class='fa fa-angle-left'></i>"+
          "</div>"+
          "</a>" +
          "</li>");
      }
      else {
        li = $("<li style='list-style: none;' class='rounded /*bg-dark*/ /*bg-gradient*/'>" +
          "<a class='Link' href='javascript:void(0)' data-url='" + this.Component + "' data-ParentId='" + this.ParentID + "' >" +
          "<i class='" + this.Icon + "'></i><span class=ms-2>" + this.Name + "</span> " +
          "</a>" +
          "</li>");
      }
      li.appendTo(Ul);
      if (this.Items && this.Items.length > 0) {
        var ul = $("<ul class=''></ul>");
        ul.addClass('nav-pills');
        ul.addClass('nav-stacked');
        ul.addClass('pe-3');
        //ul.css({ 'width': 'auto', 'margin-right': '5px', 'margin-left': '-30px', 'direction': 'ltr' });
        ul.appendTo(li);
        /*$("li>ul>li>a.Link").css({ 'width': '240px', 'float': 'right' });*/
        CreateUL(ul, this.Items);
      }
    }
  });


}

export function IsLoggedIn(): boolean {
  let LocalStorageToken = localStorage.getItem('token');
  if (LocalStorageToken == null) {

    return false;
  }
  else {
    return true;
  }
}


