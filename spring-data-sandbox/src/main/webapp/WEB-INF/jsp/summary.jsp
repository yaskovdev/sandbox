<!DOCTYPE html>

<%@ taglib prefix="spring" uri="http://www.springframework.org/tags" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<html>
<head>
    <c:url value="/js/lib/jquery-3.2.1.min.js" var="js"/>
    <script type="text/javascript" src="${js}"></script>
</head>
<body>
Message: ${message}
<button type="button" onclick="performLongRunningOperation()">Do Long Stuff</button>
<button type="button" onclick="cancel()">Cancel</button>
<script type="text/javascript">
    const performLongRunningOperation = function () {
        $.post('/notifications');
    };
    const cancel = function () {
        $.post('/cancel');
    };
</script>
</body>
</html>
