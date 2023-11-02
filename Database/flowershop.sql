use master
if exists(select * from sysdatabases where name='flowershop')
	drop database flowershop
go
create database flowershop
go
use flowershop
go 

drop table Category
drop table Product
drop table Comment
drop table Account
drop table ShoppingCart
drop table CartItem

create table Category (
	id_category varchar(30) primary key not null,
	name_category nvarchar(30),
	CssClass varchar(30)
)

create table Product (
	id_product nvarchar(30) primary key not null,
	name_product nvarchar(max),
	newcash nvarchar(max),
	--author nvarchar(100),
	oldcash nvarchar(max),
	--dealpercent float,
	--publish_date date,
	size varchar(100),
	--loai_bia nvarchar(100),
	--nha_xuat_ban nvarchar(100),
	--company nvarchar(100),
	--id_author int,
	id_cate varchar(30),	
	imageurl varchar(max),
	desproduct nvarchar(max),
	main_flower nvarchar(100),
	sub_flower nvarchar(100),
	star_sum int,
	quantity int
)

create table Comment(
	id_comment varchar(36) primary key not null,
	comment_content NTEXT,
	comment_time SMALLDATETIME,
	id_user varchar(36),
	id_flower nvarchar(30),
	star_rating int
)

create table Account(
	AccountId varchar(36) primary key not null,
	Username varchar(32),
	Password varchar(256),
	Email varchar(128),
	first_name nvarchar(30),
	last_name nvarchar(30),
	Address nvarchar(256),
	Country nvarchar(30),
	account_des nvarchar(100),
	phone_number int
)



create table ShoppingCart(
	id_cart int IDENTITY primary key not null,
	id_user varchar(36),
	cart_time SMALLDATETIME,
	total_price decimal,
	PhoneNonAccount VARCHAR(11),
	NameCusNonAccount VARCHAR(100),
	order_address nvarchar(100),
	order_name_nonAccount nvarchar(100),
	order_city nvarchar(50),
	order_phone varchar(20),
	order_shipped TINYINT default 0,
	order_email varchar(100),
	order_tracking_number varchar(50)
)
create table CartItem(
	id_CartItem int IDENTITY primary key not null,
	id_shoppingCart int not null,
	id_product nvarchar(30) not null,
	payment_id int,
	quantity_item int not null,
)

alter table Product 
add constraint fk_product_category foreign key (id_cate) references category(id_category);

alter table Comment
add constraint fk_user_id foreign key (id_user) references Account(AccountId),
constraint fk_flower_id foreign key (id_flower) references Product(id_product);

alter table ShoppingCart
add constraint fk_account_id foreign key (id_user) references Account(AccountId);

alter table CartItem
add constraint fk_shoppingcart_id foreign key (id_shoppingCart) references ShoppingCart(id_cart),
constraint fk_product_id foreign key (id_product) references Product(id_product);

INSERT INTO Category (id_category, name_category, CssClass) 
VALUES
   ('cat01', N'Hoa 20/10', ''),
   ('cat02', N'Hoa 8/3', ''),
   ('cat03', N'Hoa 20/11', ''),
   ('cat04', N'Hoa Tặng Lễ Tốt Nghiệp', ''),
   ('cat05', N'Bó Hoa Hướng Dương', '');

INSERT INTO Product(id_product,name_product,newcash,oldcash,size,id_cate,imageurl,desproduct,main_flower,sub_flower,star_sum,quantity)
VALUES
	('cat01_1',N'Em Là Duy Nhất','799.000 VNĐ',null,'45x55','cat01','/IMG/products/cat01/cat01_1/1314_1606450403-hoa-valnetine-20.png',N'Với anh, em luôn là duy nhất. Duy nhất trong trái tim của anh.
	Giỏ hoa hồng đỏ này sẽ giúp bạn gửi thông điệp về tình yêu cho người ấy vào ngày Valentine đầy hạnh phúc và ý nghĩa.',N'Hoa hồng đỏ',N'Hoa lan vũ nữ, Hoa baby',5,10),
	('cat01_2',N'20-10 Mình Có Nhau','1.450.000 VNĐ',null,'60x120','cat01','/IMG/products/cat01/cat01_2/1310_1606984910-hoa-valentine-44.png',N'Hoa hạnh phúc thể hiện được sự hạnh phúc trong tình yêu, Lan Hồ Điệp màu tím thể hiện được tình yêu chung thủy, còn hoa hồng phấn thể hiện sự ngọt ngào, lãng mạn trong tình yêu.
	Tất cả những loài hoa có mặt trong giỏ hoa này đều tôn vinh một tình yêu chân thành của bạn.',N'Hoa hồng phấn, Hoa hạnh phúc',N'Hoa lan hồ điệp',5,6),
	('cat01_3',N'Duyên Phận','950.000 VNĐ','1.000.000 VNĐ','50x100','cat01','/IMG/products/cat01/cat01_3/1309_1606534679-hoa-valentine-36.png',N'Có Duyên, có Nợ mới đến được với nhau. Ngày Valentine này hãy bày tỏ tình yêu chân thành của bạn cho nàng ấy. Một giỏ hoa gồm nhiều màu sắc khác nhau, cũng giống như cuộc sống nhiều sắc màu và khó khăn,
	trải nghiệm. Nhưng điểm đến cuối cùng vẫn là cái nắm tay của hai bạn.',N'Hoa hồng đỏ, Hoa hồng phấn, Hoa Lan',N'Hoa hồng môn, Hoa ly kép',5,8),
	('cat01_4',N'Thiên Thần Của Anh','750.000 VNĐ',null,'50x60','cat01','/IMG/products/cat01/cat01_4/1311_1606535270-hoa-valentine-37.png',N'Hoa Baby thể hiện một tinh yêu thuần khiết, trong sáng và thuần túy. Cũng giống như một thiên thần - yêu thương vô điều kiện.
	Nếu bạn thấy rằng, cô ấy tuyệt vời như vậy, hãy dành tặng cho nàng bó hoa xinh tươi này nhé!',N'Hoa baby, Hoa hồng đỏ',N'Hoa lá phụ khác',5,9);

	DELETE FROM Product
