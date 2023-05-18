var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Run(async (HttpContext context) =>
{
    double firstNumber = 0;
    bool firstNumberTrace = false;
    bool secondNumberTrace = false;
    double secondNumber = 0;

    if (context.Request.Query.ContainsKey("firstNumber"))
    {
        firstNumberTrace = true;
        firstNumber = double.Parse(context.Request.Query["firstNumber"]);//take the value
        //await context.Response.WriteAsync($"<p>firstNumber = {firstNumber}</p>");//write the value
    }
    if (context.Request.Query.ContainsKey("secondNumber"))
    {
        secondNumberTrace = true;
        secondNumber = double.Parse(context.Request.Query["secondNumber"]);//take the value
        //await context.Response.WriteAsync($"<p>secondNumber = {secondNumber}</p>");//write the value
    }
    bool operationExist = context.Request.Query.ContainsKey("operation");
    var operationInfo = context.Request.Query["operation"];
    var invalidOperation = (operationInfo != "add" && operationInfo != "subtract" && operationInfo != "multiply" && operationInfo != "divide" && operationInfo != "mod");

    if (!firstNumberTrace || !secondNumberTrace || !operationExist || invalidOperation)
    {
        if (!firstNumberTrace)
        {
            context.Response.WriteAsync($"Invalid input for 'firstNumber'");
            await context.Response.WriteAsync("\n");
        }

        if (!secondNumberTrace)
        {
            context.Response.WriteAsync($"Invalid input for 'secondNumber'");
            await context.Response.WriteAsync("\n");
        }

        if (!operationExist || invalidOperation) {
            context.Response.WriteAsync($"Invalid input for 'operation'");
            await context.Response.WriteAsync("\n");
        }

        context.Response.StatusCode = 400;
    }

    if (operationExist && firstNumberTrace && secondNumberTrace)
    {
        string operation = context.Request.Query["operation"];//take the value
        //await context.Response.WriteAsync($"<p>operation = {operation}</p>");//write the value
        double result = operation switch
        {
            "add" => firstNumber+secondNumber,
            "subtract" => firstNumber - secondNumber,
            "multiply" => firstNumber * secondNumber,
            "divide" => firstNumber / secondNumber,
            "mod" => firstNumber % secondNumber,
            _ => -1 //invalid operation
        };

        await context.Response.WriteAsync($"<p>result = {result}</p>");//write the value
    }
    
});


app.Run();
