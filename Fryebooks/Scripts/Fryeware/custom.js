'use strict';
$(function () {

    /* custom.js
     * -------
     * client side script for custom actions
     */


    //-----------------------
    //- DOCUMENT READY -
    //-----------------------
    $(document).ready(function () {
        console.log('hi');
        var mainTable = $('#tableMain').DataTable({
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/WorkResponses/GetWorkItems"
            },

            columns: [
           {
               name: 'Client',
               data: 'Client',
               title: 'Client',
               sortable: true,
               searchable: true
           },
            {
                name: 'Description',
                data: 'Description',
                title: 'Description',
                sortable: true,
                searchable: true
            },
            {
                name: 'TimeStarted',
                data: 'TimeStarted',
                title: 'Started',
                sortable: true,
                searchable: true
            },
            {
                name: 'TimeWorked',
                data: 'TimeWorked',
                title: 'TimeWorked',
                sortable: true,
                searchable: true
            },
            {
                name: 'Billable',
                data: 'Billable',
                title: 'Billable',
                sortable: true,
                searchable: true
            }
            ],
            //"columnDefs": [
            //    {
            //        "targets": 0,
            //        "data": null,
            //        "render": function (data, type, row, meta) {
            //            var reasonVal = "Reciept";
            //            if (row.Reason === 1) {
            //                reasonVal = "Scrap";
            //            }
            //            if (row.Reason === 2) {
            //                reasonVal = "Adjustment";
            //            }
            //            return reasonVal;
            //        }
            //    },
            //    {
            //        "targets": 6,
            //        "data": null,
            //        "render": function (data, type, row, meta) {
            //            return '<div class="pull-right"><a href="#" data-adjustmentid=' + row.ID + ' class="btn btn-success btn-sm action-editItem" data-toggle="modal" data-target="#editModal">Edit</a></div>';
            //        }
            //    }]
        });


        $('#closeItemEditBtn').click(function () {
            adjustmentTable.ajax.reload();
        });
    });
    //-----------------------
    //- END DOCUMENT READY -
    //-----------------------

    //-----------------------
    //- NEW ADJUSTMENT MODAL -
    //-----------------------


    //----------------------------
    //- END NEW ADJUSTMENT MODAL -
    //----------------------------

});