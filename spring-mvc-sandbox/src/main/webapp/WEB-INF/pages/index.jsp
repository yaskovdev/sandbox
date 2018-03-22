<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>

<form:form action="${pageContext.request.contextPath}/insurances" modelAttribute="form" method="post">
    <fmt:formatDate var="date" value="${form.insuranceStartDate}" pattern="dd.MM.yyyy"/>
    <form:input path="insuranceStartDate" type="text" value="${date}" required="required"/>
    <input type="submit" value="Submit">
</form:form>