<script>
    var amsurl = "http://localhost:44348";
    $("#btnclick").click(function () {

            var CatName = $("#CatName").val();
    var CID = $("#CID").val();



    $.ajax({
        url: amsurl + '/Category/AddCategory',
    method: "GET",
    data: {CID: CID, CatName: CatName },
    success: function (data) {
                    var htm = ``;
    if (data != null) {
        htm += `
            

               <div class="widget-content widget-content-area">
                    
                      <label>Category:</label>
                        <p class="form-control-plaintext text-muted">`+ data.CatName + `</p>
                        <p class="form-control-plaintext text-muted" hidden>`+ data.CID + `</p>
<div class="form-group">
                                        <label>
                                            Input Sub Category Name:
                                            <span class="text-danger">*</span>
                                        </label>
                                        
                                    </div>
               </div>
                
                    <input type="text" id="SubCatName" name="SubCat"><br><br>

                
               <div class="widget-footer text-right">
                                    <button type="reset" class="btn btn-primary mr-2" onclick="subbtnclick">Submit</button>
                                    <button type="reset" class="btn btn-outline-primary">Cancel</button>
                                </div>

            `;
                    }
    else {
        alert('error');
    htm = `No User Found`
                    }
    $('.SubCatArea').html(htm);
                }

            });
        });
    $("#subbtnclick").click(function () {
            var SubCatName = $("#SubCatName").val();
    var CID = $("#CID").val();
    $.ajax({
        url: amsurl + '/Category/AddSubCat',
    type: 'post',
    data: {SubCatName: SubCatName, CID: CID },
    success: function (data) {
        alert(data);
    var htm = ``;
    if (data != null) {
        window.localtion = amsurl + '/Home/Index';
                    }
    else {
        alert('error')
    }
                }
            });
        });
</script>





  
  




