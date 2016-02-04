<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CartSite.aspx.cs" Inherits="MyBookShop.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="Css/jquery-ui-1.8.16.custom.css" rel="stylesheet" />
    <script src="js/jquery-1.7.1.js"></script>
    <script src="js/jquery-ui-1.8.2.custom.min.js"></script>
    <script>
        var html = '<tr class="align_Center">' +
                        '<td style="padding: 5px 0 5px 0;">' +
                            '<img src="images/bookcovers/$ISBN.jpg" width="40" height="50" border="0" />' +
                        '</td>' +
                        '<td class="align_Left">' +
                            '$TITLE' +
                        '</td>' +
                        '<td>' +
                            '<span class="price">$UNITPRICE</span>' +
                        '</td>' +
                        '<td>' +
                            '<a href="#none" title="减一" onclick="changeBar(0,$ID,$BOOKID)" style="margin-right: 2px;">' +
                                '<img src="/Images/bag_close.gif" width="9" height="9" border="none" style="display: inline" />' +
                            '</a>' +
                            '<input type="text" id="txtCount$BOOKID" name="txtCount$BOOKID" maxlength="3" style="width: 30px" onkeydown="if(event.keyCode == 13) event.returnValue = false" value="$COUNT" onfocus="changeTxtOnFocus(this);" onblur="changeTextOnBlur(this,$ID,$BOOKID);" />' +
                            '<a href="#none" title="加一" onclick="changeBar(1,$ID,$BOOKID)" style="margin-left: 2px;">' +
                                '<img src="/images/bag_open.gif" width="9" height="9" border="none" style="display: inline" />' +
                            '</a>' +
                        '</td>' +
                        '<td>' +
                            '<a href="#none" id="btn_del_$BOOKID" onclick="removeProductOnShoppingCart($ID,$BOOKID)">删除</a>' +
                        '</td>' +
                    '</tr>';
        var hideCount = 0;//文本框数字存储

        $(function () {
            //加载所有的购物车信息
            LoadAllCartInfo();
        });

        //计算总价
        function CalculateTotalMoney() {
            var sum = 0;
            $('.align_Center:gt(0)').each(function () {
                var price = $(this).find('.price').text();
                var count = $(this).find('input').val();
                sum += parseInt(count) * parseFloat(price);
            });
            $('#totleMoney').text(sum);
        }

        //点击删除按钮
        function removeProductOnShoppingCart(cartId, bookId) {
            if (confirm('是否删除？')) {
                DeleteCartInfo(cartId, bookId);
            }
        }

        //当文本框的数字改变时，更新数据库
        function changeBar(operator, cartId, bookId) {
            var count = $('#txtCount' + bookId).val();
            if (operator == '0') {
                count--;
            }
            else if (operator == '1') {
                count++;
            } else {
                //运算符为空不做任何操作，留着以后可以增加
            }
            if (count <= 0) {
                if (confirm('是否删除？')) {
                    DeleteCartInfo(cartId, bookId);//小于0则删除
                    return;
                } else {
                    count = 1;
                }
            }
            if (count >= 1000) {
                count = 999;
            }
            $('#txtCount' + bookId).val(count);
            UpdateCount(cartId, bookId, count);
        }

        //更新==判断是更新数据库还是cookie
        function UpdateCount(cartId, bookId, count) {
            var paras;
            if (cartId != 0) {//cartId不为0表示登入状态，更新数据库
                paras = {
                    TransCode: 'UpdateDataBaseCartInfo',
                    CartId: cartId,
                    BookId: bookId,
                    Count: count
                };
            } else {//cartId为0表示未登入状态，更新cookie
                paras = {
                    TransCode: 'UpdateCookieCartInfo',
                    BookId: bookId,
                    Count: count
                };
            }
            $.post(
                'CartSite.aspx',
                paras,
                function (data) {
                    //alert(data);//测试阶段直接弹出
                    $('#spanMsg').text(data);
                    CalculateTotalMoney();
                }
            );
        }

        //删除==判断是删除数据库还是cookie
        function DeleteCartInfo(cartId, bookId) {
            var paras;
            if (cartId != 0) {//cartId不为0表示登入状态，删除数据库
                paras = {
                    TransCode: 'DeleteDataBaseCartInfo',
                    CartId: cartId
                };
            } else {//cartId为0表示未登入状态，删除cookie
                paras = {
                    TransCode: 'DeleteCookieCartInfo',
                    BookId: bookId
                };
            }
            $.post(
                'CartSite.aspx',
                paras,
                function (data) {
                    //alert(data);//测试阶段直接弹出
                    $('#spanMsg').text(data);
                    $('#txtCount' + bookId).parent().parent().remove();
                    CheckTableEmpty();
                    CalculateTotalMoney();
                }
            );
        }

        //当文本框获得焦点时，将数据保存到一个全局变量
        function changeTxtOnFocus(control) {
            hideCount = $(control).val();
        }

        //当文本框失去焦点时，判断文本框中的数据是否数字，否则将全局变量给文本框
        function changeTextOnBlur(control, cartId, bookId) {
            var len = $(control).length;
            var reg = /^[0-9]*$/; //构造正则表达式,第一个如果是0，则删除商品，其他必须是数字
            if (reg.test($(control).val())) {
                hideCount = $(control).val();
                changeBar('', cartId, bookId);//一旦更改，调用更改的方法。运算符传个空值即可
            } else {
                alert('请输入数字');
                $(control).val(hideCount);
            }
        }

        //加载所有的购物车信息
        function LoadAllCartInfo() {
            $('#tableCartInfo tr:gt(0)').remove();//加载之前清空购物车信息
            $.getJSON(
                'CartSite.aspx',
                {
                    TransCode: 'LoadAllCartInfo'
                },
                function (data) {
                    if (data != null) {
                        $.each(data, function (index, item) {
                            var newHtml = html.replace(/[$]ISBN/g, item.Book.ISBN).replace(/[$]TITLE/g, item.Book.Title).replace(/[$]UNITPRICE/g, item.Book.UnitPrice).replace(/[$]BOOKID/g, item.Book.Id).replace(/[$]COUNT/g, item.Count).replace(/[$]ID/g, item.Id);
                            $(newHtml).appendTo('#tableCartInfo');
                        });
                    }
                    CheckTableEmpty();
                    CalculateTotalMoney();
                }
            );
        }

        //判断购物车中是否有商品，如果没有，就显示请添加商品.注意table有一个子元素是tbody！！！
        function CheckTableEmpty() {
            if ($('#tableCartInfo tbody').children().length == 1) {
                $('#tableCartInfo').hide();
                $('#tableMyCartInfo tr').last().hide();
                $('#divEmptyCart').show();
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <table id="tableMyCartInfo" cellpadding="0" cellspacing="0" width="98%">
            <tr>
                <td colspan="2">
                    <img height="27"
                        src="images/shop-cart-header-blue.gif" width="206" /><img alt=""
                            src="Images/png-0170.png" /><asp:HyperLink ID="HyperLink1" runat="server"
                                NavigateUrl="~/myorder.aspx">我的订单</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" width="98%">

                    <table id="tableCartInfo" cellpadding='0' cellspacing='0' width='100%'>
                        <tr class='align_Center Thead'>
                            <td width='7%' style='height: 30px'>图片</td>
                            <td>图书名称</td>
                            <td width='14%'>单价</td>
                            <td width='11%'>购买数量</td>
                            <td width='7%'>删除图书</td>
                        </tr>

                        <!--一行数据的开始 -->

                        <!--一行数据的结束 -->

                        <tr>
                            <td class='align_Right Tfoot' colspan='5' style='height: 30px'>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td style="text-align: center">&nbsp;&nbsp;&nbsp; 商品金额总计：<span id="totleMoney">0</span>元</td>
                <td>&nbsp;
               <a href="/Index.aspx">
                   <img alt="" src="Images/gobuy.jpg" width="103" height="36" border="0" />
               </a><a href="/OrderConfirm.aspx">
                   <img src="images/balance.gif"
                       border="0" /></a><br />
                    <span id="spanMsg" style="font-size: 18px; color: blue; font-weight: bolder;"></span>
                </td>
            </tr>
        </table>
    </div>

    <br />
    <br />
    <div id="divEmptyCart" style="display: none;">
        <span style="font-size: 18px; color: black;">啊哦！您还没有购买任何商品</span>
        <br />
        <br />
        <a href="/Index.aspx" style="font-size: 24px; color: blue; font-weight: bolder;">立即前往首页选购？</a>
    </div>
    <br />
    <br />

</asp:Content>
