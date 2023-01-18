function ErrMsg() {    
    var CorEmail = document.getElementById("MainContent$FromMail").value;

    if (CorEmail == "") 
     {       
            alert('Please enter email.');
            return false;
     }
    else 
    {
            try {
                var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                if (reg.test(CorEmail) == false)
                {
                    alert("Email address is invalid");
                    return false;
                }
            }
           catch (err)
           {
                alert(err.message);
            }
    }
    alert("Email address is invalid");
    return true;
}
    
