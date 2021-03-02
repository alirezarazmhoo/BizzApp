function AutoComplete(parameter) {
	var input = document.getElementById('idTxtCategorySearch');
	if (input.value.length == 0) {
		$('#cmbSearchCategoryAlternative').empty();
		$('#cmbSearchCategoryAlternative').css("display", "none");
		$('#cmbSearchCategoryPrimary').css("display", "block");
	}
	else {
		$('#cmbSearchCategoryPrimary').css("display", "none");
		$('#cmbSearchCategoryAlternative').empty();
		$('#cmbSearchCategoryAlternative').css("display", "block");
		$.ajax({
			type: "GET",
			url: '/Home/SearchCategory?txtSearch=' + parameter + '',
			dataType: "json",
			contentType: false,
			processData: false,
			success: function (response) {
				$.each(response.categories, function () {
					$('#cmbSearchCategoryAlternative').append($("<li><a href=#this.id style='margin-right:15px'>" + this.name + "<a><li>"));
				});

			},
		});
	}
}

function CityAutoComplete(parameter) {
	$('#cmbSearchCity').empty();

	$.ajax({
		type: "GET",
		url: 'Home/GetAllWithParentNames?txtSearch=' + parameter + '',
		dataType: "json",
		contentType: false,
		processData: false,
		success: function (response) {
			$.each(response.districts, function () {

				$('#cmbSearchCity').append($("<li><a href=#this.id style='margin-right:15px'>" + this.listName + "<a><li>"));
			});
			$('#cmbSearchCity').click();

		},
	});


}
