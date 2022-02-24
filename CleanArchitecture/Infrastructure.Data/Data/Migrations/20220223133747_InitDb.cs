using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Data.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<byte>(type: "tinyint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Component = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSelective = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMenus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenus_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "ID", "Component", "Icon", "IsSelective", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, null, "fa fa-chevron-circle-down", true, "پروفایل", null },
                    { 2, "Account", "fa fa-male", true, "ویرایش مشخصات", (byte)1 },
                    { 3, null, "fas fa-cogs", true, "تنظیمات", (byte)1 },
                    { 4, null, "fa fa-chevron-circle-down", true, "تعاریف حسابداری", null },
                    { 5, "GeneralOffice", "fa fa-book", true, "کل", (byte)4 },
                    { 6, "DefiniteOffice", "fa fa-sticky-note-o", true, "معین", (byte)4 }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ID", "Name", "PrePhoneNumber" },
                values: new object[,]
                {
                    { 19, "قم", "025" },
                    { 20, "كردستان", "087" },
                    { 21, "كرمان", "034" },
                    { 22, "كرمانشاه", "083" },
                    { 23, "كهگیلویه و بویراحمد", "047" },
                    { 24, "گلستان", "017" },
                    { 27, "مازندران", "011" },
                    { 26, "لرستان", "066" },
                    { 18, "قزوین", "028" },
                    { 28, "مركزی", "086" },
                    { 29, "هرمزگان", "076" },
                    { 30, "همدان", "081" },
                    { 31, "یزد", "035" },
                    { 25, "گیلان", "013" },
                    { 17, "فارس", "071" },
                    { 15, "سمنان", "023" },
                    { 6, "ایلام", "084" },
                    { 14, "زنجان", "024" },
                    { 13, "خوزستان", "061" },
                    { 12, "خراسان شمالی", "058" },
                    { 11, "خراسان رضوی", "051" },
                    { 10, "خراسان جنوبی", "066" },
                    { 9, "چهارمحال و بختیاری", "038" },
                    { 8, "تهران", "021" },
                    { 7, "بوشهر", "077" },
                    { 16, "سیستان و بلوچستان", "054" },
                    { 5, "البرز", "026" },
                    { 4, "اصفهان", "031" },
                    { 3, "اردبیل", "045" },
                    { 2, "آذربایجان غربی", "044" },
                    { 1, "آذربایجان شرقی", "041" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 2, "49281c4d-7294-4192-9228-9542b0cfe94e", "فروشنده", "فروشنده" },
                    { 3, "a5adcc01-7caa-41dc-9aa4-764c9e7168de", "خریدار", "خریدار" },
                    { 1, "f9d60d20-7da3-427f-9d89-582a7c4d627e", "مدیر سیستم", "مدیر سیستم" },
                    { 4, "e4635653-eb5a-492d-a1b8-a26e6c802a21", "بازاریاب", "بازاریاب" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 1, "آذرشهر", 1 },
                    { 338, "هرسین", 22 },
                    { 337, "گیلان غرب", 22 },
                    { 336, "كنگاور", 22 },
                    { 335, "كرمانشاه", 22 },
                    { 334, "قصر شیرین", 22 },
                    { 333, "صحنه", 22 },
                    { 332, "سنقر", 22 },
                    { 331, "سر پل ذهاب", 22 },
                    { 330, "روانس", 22 },
                    { 329, "دالاهو", 22 },
                    { 328, "جوانرود", 22 },
                    { 327, "ثلاث باباجانی", 22 },
                    { 326, "پاوه", 22 },
                    { 325, "اسلام آباد غرب", 22 },
                    { 250, "هرسین", 22 },
                    { 249, "گیلان غرب", 22 },
                    { 248, "كنگاور", 22 },
                    { 247, "كرمانشاه", 22 },
                    { 246, "قصر شیرین", 22 },
                    { 245, "صحنه", 22 },
                    { 244, "سنقر", 22 },
                    { 243, "سر پل ذهاب", 22 },
                    { 242, "روانسر", 22 },
                    { 251, "بویر احمد", 23 },
                    { 241, "دالاهو", 22 },
                    { 252, "بهمئی", 23 },
                    { 254, "كهگیلویه", 23 },
                    { 349, "علی آباد", 24 },
                    { 348, "رامیان", 24 },
                    { 347, "بندر تركمن", 24 },
                    { 346, "بندر گز", 24 },
                    { 345, "آق قلا", 24 },
                    { 344, "آزادشهر", 24 },
                    { 266, "مینو دشت", 24 },
                    { 265, "گنبد كاووس", 24 },
                    { 264, "گرگان", 24 },
                    { 263, "كلاله", 24 },
                    { 262, "كرد كوی", 24 },
                    { 261, "علی آباد", 24 },
                    { 260, "رامیان", 24 },
                    { 259, "بندر تركمن", 24 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 258, "بندر گز", 24 },
                    { 257, "آق قلا", 24 },
                    { 256, "آزادشهر", 24 },
                    { 343, "گچساران", 23 },
                    { 342, "كهگیلویه", 23 },
                    { 341, "دنا", 23 },
                    { 340, "بهمئی", 23 },
                    { 339, "بویر احمد", 23 },
                    { 255, "گچساران", 23 },
                    { 253, "دنا", 23 },
                    { 240, "جوانرود", 22 },
                    { 239, "ثلاث باباجانی", 22 },
                    { 238, "پاوه", 22 },
                    { 225, "راور", 21 },
                    { 224, "جیرفت", 21 },
                    { 223, "بم", 21 },
                    { 222, "بردسیر", 21 },
                    { 221, "بافت", 21 },
                    { 308, "مریوان", 20 },
                    { 307, "كامیاران", 20 },
                    { 306, "قروه", 20 },
                    { 305, "سنندج", 20 },
                    { 304, "سقز", 20 },
                    { 303, "سروآباد", 20 },
                    { 302, "دیواندره", 20 },
                    { 301, "بیجار", 20 },
                    { 300, "بانه", 20 },
                    { 220, "مریوان", 20 },
                    { 219, "كامیاران", 20 },
                    { 218, "قروه", 20 },
                    { 217, "سنندج", 20 },
                    { 216, "سقز", 20 },
                    { 215, "سروآباد", 20 },
                    { 214, "دیواندره", 20 },
                    { 213, "بیجار", 20 },
                    { 212, "بانه", 20 },
                    { 226, "رفسنجان", 21 },
                    { 227, "رودبار جنوب", 21 },
                    { 228, "زرند", 21 },
                    { 229, "سیرجان", 21 },
                    { 237, "اسلام آباد غرب", 22 },
                    { 324, "منوجان", 21 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 323, "كهنوج", 21 },
                    { 322, "كوهبنان", 21 },
                    { 321, "كرمان", 21 },
                    { 320, "قلعه گنج", 21 },
                    { 319, "عنبرآباد", 21 },
                    { 318, "شهر بابك", 21 },
                    { 317, "سیرجان", 21 },
                    { 316, "زرند", 21 },
                    { 315, "رودبار جنوب", 21 },
                    { 350, "كرد كوی", 24 },
                    { 314, "رفسنجان", 21 },
                    { 312, "جیرفت", 21 },
                    { 311, "بم", 21 },
                    { 310, "بردسیر", 21 },
                    { 309, "بافت", 21 },
                    { 236, "منوجان", 21 },
                    { 235, "كهنوج", 21 },
                    { 234, "كوهبنان", 21 },
                    { 233, "كرمان", 21 },
                    { 232, "قلعه گنج", 21 },
                    { 231, "عنبرآباد", 21 },
                    { 230, "شهر بابك", 21 },
                    { 313, "راور", 21 },
                    { 351, "كلاله", 24 },
                    { 352, "گرگان", 24 },
                    { 353, "گنبد كاووس", 24 },
                    { 409, "بندرعباس", 29 },
                    { 408, "محلات", 28 },
                    { 407, "كمیجان", 28 },
                    { 406, "شازند", 28 },
                    { 405, "ساوه", 28 },
                    { 404, "زرندیه", 28 },
                    { 403, "دلیجان", 28 },
                    { 402, "خمین", 28 },
                    { 401, "تفرش", 28 },
                    { 400, "اراك", 28 },
                    { 399, "آشتیان", 28 },
                    { 398, "فریدونكنار", 27 },
                    { 397, "نوشهر", 27 },
                    { 396, "نور", 27 },
                    { 395, "نكا", 27 },
                    { 394, "محمود آباد", 27 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 393, "گلوگاه", 27 },
                    { 392, "قائم شهر", 27 },
                    { 391, "سوادكوه", 27 },
                    { 390, "ساری", 27 },
                    { 389, "رامسر", 27 },
                    { 388, "چالوس", 27 },
                    { 387, "جویبار", 27 },
                    { 410, "میناب", 29 },
                    { 411, "بندر لنگه", 29 },
                    { 412, "رودان-دهبارز", 29 },
                    { 413, "جاسك", 29 },
                    { 437, "یزد", 31 },
                    { 436, "میبد", 31 },
                    { 435, "مهریز", 31 },
                    { 434, "طبس", 31 },
                    { 433, "صدوق", 31 },
                    { 432, "خاتم", 31 },
                    { 431, "تفت", 31 },
                    { 430, "بافق", 31 },
                    { 429, "اردكان", 31 },
                    { 428, "ابركوه", 31 },
                    { 427, "همدان", 30 },
                    { 386, "تنكابن", 27 },
                    { 426, "نهاوند", 30 },
                    { 424, "كبودر آهنگ", 30 },
                    { 423, "رزن", 30 },
                    { 422, "تویسركان", 30 },
                    { 421, "بهار", 30 },
                    { 420, "اسدآباد", 30 },
                    { 419, "خمیر", 29 },
                    { 418, "گاوبندی", 29 },
                    { 417, "بستك", 29 },
                    { 416, "ابوموسی", 29 },
                    { 415, "حاجی آباد", 29 },
                    { 414, "قشم", 29 },
                    { 425, "ملایر", 30 },
                    { 299, "قم", 19 },
                    { 385, "بهشهر", 27 },
                    { 383, "بابل", 27 },
                    { 360, "رضوانشهر", 25 },
                    { 359, "رشت", 25 },
                    { 358, "بندر انزلی", 25 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 357, "املش", 25 },
                    { 356, "آستانه اشرفیه", 25 },
                    { 355, "آستارا", 25 },
                    { 282, "ماسال", 25 },
                    { 281, "لنگرود", 25 },
                    { 280, "لاهیجان", 25 },
                    { 279, "فومن", 25 },
                    { 278, "طوالش", 25 },
                    { 277, "صومعه سرا", 25 },
                    { 276, "شفت", 25 },
                    { 275, "سیاهكل", 25 },
                    { 274, "رودسر", 25 },
                    { 273, "رودبار", 25 },
                    { 272, "رضوانشهر", 25 },
                    { 271, "رشت", 25 },
                    { 270, "بندر انزلی", 25 },
                    { 269, "املش", 25 },
                    { 268, "آستانه اشرفیه", 25 },
                    { 267, "آستارا", 25 },
                    { 354, "مینو دشت", 24 },
                    { 361, "رودبار", 25 },
                    { 362, "رودسر", 25 },
                    { 363, "سیاهكل", 25 },
                    { 364, "شفت", 25 },
                    { 382, "آمل", 27 },
                    { 381, "نورآباد", 26 },
                    { 380, "الشتر", 26 },
                    { 379, "كوهدشت", 26 },
                    { 378, "سلسله", 26 },
                    { 377, "دلفان", 26 },
                    { 376, "دورود", 26 },
                    { 375, "خرم آباد", 26 },
                    { 374, "پلدختر", 26 },
                    { 373, "بروجرد", 26 },
                    { 372, "الیگودرز", 26 },
                    { 384, "بابلسر", 27 },
                    { 371, "ازنا", 26 },
                    { 287, "خرم آباد", 26 },
                    { 286, "پلدختر", 26 },
                    { 285, "بروجرد", 26 },
                    { 284, "الیگودرز", 26 },
                    { 283, "ازنا", 26 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 370, "ماسال", 25 },
                    { 369, "لنگرود", 25 },
                    { 368, "لاهیجان", 25 },
                    { 367, "فومن", 25 },
                    { 366, "طوالش", 25 },
                    { 365, "صومعه سرا", 25 },
                    { 288, "دورود", 26 },
                    { 298, "قزوین", 18 },
                    { 211, "قم", 19 },
                    { 296, "بوئین زهرا", 18 },
                    { 79, "دشتستان", 7 },
                    { 78, "جم", 7 },
                    { 77, "تنگستان", 7 },
                    { 76, "بوشهر", 7 },
                    { 75, "مهران", 6 },
                    { 74, "شیران و چرداول", 6 },
                    { 73, "دهلران", 6 },
                    { 72, "دره شهر", 6 },
                    { 71, "ایوان", 6 },
                    { 70, "ایلام", 6 },
                    { 69, "آبدانان", 6 },
                    { 68, "طالقان", 5 },
                    { 67, "نظرآباد", 5 },
                    { 66, "كرج", 5 },
                    { 65, "ساوجبلاق", 5 },
                    { 64, "نطنز", 4 },
                    { 63, "نجف آباد", 4 },
                    { 62, "نائین", 4 },
                    { 61, "مباركه", 4 },
                    { 60, "لنجان", 4 },
                    { 59, "گلپایگان", 4 },
                    { 58, "كاشان", 4 },
                    { 57, "فلاورجان", 4 },
                    { 80, "دشتی", 7 },
                    { 81, "دیر", 7 },
                    { 82, "دیلم", 7 },
                    { 83, "كنگان", 7 },
                    { 107, "نهبندان", 10 },
                    { 106, "قائن", 10 },
                    { 105, "فردوس", 10 },
                    { 104, "سر بیشه", 10 },
                    { 103, "سرایان", 10 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 102, "درمیان", 10 },
                    { 101, "بیرجند", 10 },
                    { 100, "لردگان", 9 },
                    { 99, "كوهرنگ", 9 },
                    { 98, "فارسان", 9 },
                    { 97, "شهركرد", 9 },
                    { 56, "فریدون شهر", 4 },
                    { 96, "بروجن", 9 },
                    { 94, "ورامین", 8 },
                    { 93, "فیروزكوه", 8 },
                    { 92, "شهریار", 8 },
                    { 91, "شمیرانات", 8 },
                    { 90, "ری", 8 },
                    { 89, "رباط كریم", 8 },
                    { 88, "دماوند", 8 },
                    { 87, "تهران", 8 },
                    { 86, "پاكدشت", 8 },
                    { 85, "اسلام شهر", 8 },
                    { 84, "گناوه", 7 },
                    { 95, "اردل", 9 },
                    { 108, "برد سكن", 11 },
                    { 55, "فریدن", 4 },
                    { 53, "شهر رضا", 4 },
                    { 24, "تكاب", 2 },
                    { 23, "پیر انشهر", 2 },
                    { 22, "بوكان", 2 },
                    { 21, "اشنویه", 2 },
                    { 20, "ارومیه", 2 },
                    { 19, "هشترود", 1 },
                    { 18, "هریس", 1 },
                    { 17, "ورزقان", 1 },
                    { 16, "میانه", 1 },
                    { 15, "ملكان", 1 },
                    { 14, "مرند", 1 },
                    { 13, "مراغه", 1 },
                    { 12, "كلیبر", 1 },
                    { 11, "عجبشیر", 1 },
                    { 10, "شبستر", 1 },
                    { 9, "سراب", 1 },
                    { 8, "چاراویماق", 1 },
                    { 7, "جلفا", 1 },
                    { 6, "تبریز", 1 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 5, "بناب", 1 },
                    { 4, "بستان آباد", 1 },
                    { 3, "اهر", 1 },
                    { 2, "اسكو", 1 },
                    { 25, "چالدران", 2 },
                    { 26, "خوی", 2 },
                    { 27, "سردشت", 2 },
                    { 28, "سلماس", 2 },
                    { 52, "شاهین شهر و میمه", 4 },
                    { 51, "سمیرم", 4 },
                    { 50, "خوانسار", 4 },
                    { 49, "خمینی شهر", 4 },
                    { 48, "چادگان", 4 },
                    { 47, "تیران و كرون", 4 },
                    { 46, "برخوار و میمه", 4 },
                    { 45, "اصفهان", 4 },
                    { 44, "اردستان", 4 },
                    { 43, "آران و بیدگل", 4 },
                    { 42, "نیر", 3 },
                    { 54, "سمیرم سفلی", 4 },
                    { 41, "نمین", 3 },
                    { 39, "گرمی", 3 },
                    { 38, "كوثر", 3 },
                    { 37, "خلخال", 3 },
                    { 36, "پارس آباد", 3 },
                    { 35, "بیله سوار", 3 },
                    { 34, "اردبیل", 3 },
                    { 33, "نقده", 2 },
                    { 32, "میاندوآب", 2 },
                    { 31, "مهاباد", 2 },
                    { 30, "ماكو", 2 },
                    { 29, "شاهین دژ", 2 },
                    { 40, "مشگین", 3 },
                    { 297, "تاكستان", 18 },
                    { 109, "بجستان", 11 },
                    { 111, "تحت جلگه", 11 },
                    { 189, "خرم بید", 17 },
                    { 188, "جهرم", 17 },
                    { 187, "پاسارگاد", 17 },
                    { 186, "بوانات", 17 },
                    { 185, "اقلید", 17 },
                    { 184, "استهبان", 17 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 183, "ارسنجان", 17 },
                    { 182, "آباده", 17 },
                    { 181, "نیكشهر", 16 },
                    { 180, "كنارك", 16 },
                    { 179, "سرباز", 16 },
                    { 178, "سراوان", 16 },
                    { 177, "زهك", 16 },
                    { 176, "زاهدان", 16 },
                    { 175, "زابل", 16 },
                    { 174, "دلگان", 16 },
                    { 173, "خاش", 16 },
                    { 172, "چابهار", 16 },
                    { 171, "ایرانشهر", 16 },
                    { 170, "مهدی شهر", 15 },
                    { 169, "گرمسار", 15 },
                    { 168, "شاهرود", 15 },
                    { 167, "سمنان", 15 },
                    { 190, "خنج", 17 },
                    { 191, "داراب", 17 },
                    { 192, "زرین دشت", 17 },
                    { 193, "سپیدان", 17 },
                    { 295, "البرز", 18 },
                    { 294, "آبیك", 18 },
                    { 210, "قزوین", 18 },
                    { 209, "تاكستان", 18 },
                    { 208, "بوئین زهرا", 18 },
                    { 207, "البرز", 18 },
                    { 206, "آبیك", 18 },
                    { 293, "نی ریز", 17 },
                    { 292, "مهر", 17 },
                    { 291, "ممسنی", 17 },
                    { 290, "مرودشت", 17 },
                    { 110, "تایباد", 11 },
                    { 289, "لامرد", 17 },
                    { 204, "مهر", 17 },
                    { 203, "ممسنی", 17 },
                    { 202, "مرودشت", 17 },
                    { 201, "لامرد", 17 },
                    { 200, "لارستان", 17 },
                    { 199, "كازرون", 17 },
                    { 198, "قیر و كارزین", 17 },
                    { 197, "فیروزآباد", 17 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 196, "فسا", 17 },
                    { 195, "فراشبند", 17 },
                    { 194, "شیراز", 17 },
                    { 205, "نی ریز", 17 },
                    { 165, "ماه نشان", 14 },
                    { 166, "دامغان", 15 },
                    { 163, "زنجان", 14 },
                    { 134, "بجنورد", 12 },
                    { 133, "اسفراین", 12 },
                    { 132, "نیشابور", 11 },
                    { 131, "مه ولات", 11 },
                    { 130, "مشهد", 11 },
                    { 129, "گناباد", 11 },
                    { 128, "كلات", 11 },
                    { 127, "كاشمر", 11 },
                    { 126, "طرقبه و شاندیز", 11 },
                    { 125, "قوچان", 11 },
                    { 124, "فریمان", 11 },
                    { 123, "سرخس", 11 },
                    { 122, "سبزوار", 11 },
                    { 121, "زاوه", 11 },
                    { 120, "رشتخوار", 11 },
                    { 119, "درگز", 11 },
                    { 118, "خواف", 11 },
                    { 117, "خلیل آباد", 11 },
                    { 116, "جوین", 11 },
                    { 115, "جغتای", 11 },
                    { 114, "چناران", 11 },
                    { 113, "تربت حیدریه", 11 },
                    { 112, "تربت جام", 11 },
                    { 164, "طارم", 14 },
                    { 136, "شیروان", 12 },
                    { 135, "جاجرم", 12 },
                    { 138, "مانه و سملقان", 12 },
                    { 162, "خرمدره", 14 },
                    { 161, "خدابنده", 14 },
                    { 160, "ایجرود", 14 },
                    { 159, "ابهر", 14 },
                    { 158, "هندیجان", 13 },
                    { 157, "مسجد سلیمان", 13 },
                    { 156, "لالی", 13 },
                    { 155, "گتوند", 13 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 154, "شوشتر", 13 },
                    { 153, "شوش", 13 },
                    { 137, "فاروج", 12 },
                    { 151, "رامهرمز", 13 },
                    { 152, "شادگان", 13 },
                    { 149, "دشت آزادگان", 13 },
                    { 148, "دزفول", 13 },
                    { 147, "خرمشهر", 13 },
                    { 146, "بهبهان", 13 },
                    { 145, "بندرماهشهر", 13 },
                    { 144, "باغ ملك", 13 },
                    { 143, "ایذه", 13 },
                    { 142, "اهواز", 13 },
                    { 141, "اندیمشك", 13 },
                    { 140, "امیدیه", 13 },
                    { 139, "آبادان", 13 },
                    { 150, "رامشیر", 13 }
                });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "ID", "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 3, 3, 1 },
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 4, 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_MenuId",
                table: "RoleMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_RoleId",
                table: "RoleMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenus_MenuId",
                table: "UserMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenus_UserId",
                table: "UserMenus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "UserMenus");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
