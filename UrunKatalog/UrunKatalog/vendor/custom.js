
$(document).ready(function () {

    $("#tblProduct").on("click",".btnProductDelete", function () {

        var btn = $(this);

        bootbox.confirm("Ürünü silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var id = btn.data("id");

                $.ajax({
                    type: "GET",
                    url: "/AdminProduct/Delete/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                    }
                })
            }
        })

    })

})

