$(document).ready(function () {
	// Initialize ajax autocomplete for cities:
	// HELP: https://www.devbridge.com/sourcery/components/jquery-autocomplete/
	$('#city-autocomplete').autocomplete({
		serviceUrl: '/cities/getCitiesWithProviceNames',
		minChars: 2,
		paramName: 'searchString',
		transformResult: function (response) {
			var result = JSON.parse(response);
			return {
				suggestions: $.map(result, function (dataItem) {
					return { data: dataItem.value, value: dataItem.text };
				})
			};
		},
		type: "get",
		onSelect: function (suggestion) {
			$('#selectedCity').html('<b>شهر: </b><i>' + suggestion.value + '</i>');
			$('#Input_CityId').attr('value', suggestion.data);
		},
		onInvalidateSelection: function () {
			$('#selectedCity').html('دسته انتخاب شده: هیچ');
			$('#Input_CityId').attr('value', 0);
		},
		showNoSuggestionNotice: true,
		noSuggestionNotice: "متاسفانه موردی پیدا نشد"
	});

});