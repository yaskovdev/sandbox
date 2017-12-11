<%@ taglib prefix="spring" uri="http://www.springframework.org/tags"%>

<br>
<label for="n1">first number:</label>
<input type="text" class="form-control" id="n1">
<label for="n2">second number:</label>
<input type="text" class="form-control" id="n2">
<br>
<button type="button" id="myBtn" class="btn btn-success">Add</button>
<script type="text/javascript" src="<spring:url value="/resources/js/cloudnet.js"/>"></script>
<%-- <button type="submit" class="btn btn-primary" href="<c:url value="/addItem.jsp"/>">Click</button> --%>
</div>