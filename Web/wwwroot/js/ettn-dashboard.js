"use strict";
function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { ownKeys(source, true).forEach(function (key) { _defineProperty(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { ownKeys(source).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }



$(document).ready(function () {
    // Chart in Dashboard version 1
    var echartElemBar = document.getElementById('echartBarByWeek');

    if (echartElemBar) {
        $.ajax({
            type: 'GET',
            url: '/Ettn/Home/GetAllDocumentsByWeek',
            dataType: 'json',
            success: function (response) {
                var echartBar = echarts.init(echartElemBar);

                echartBar.setOption({
                    legend: {
                        borderRadius: 0,
                        orient: 'horizontal',
                        x: 'right',
                        data: ['Реализованные', 'Оприходованные', 'Списанные', 'Перемещенные']
                    },
                    grid: {
                        left: '8px',
                        right: '8px',
                        bottom: '0',
                        containLabel: true
                    },
                    tooltip: {
                        show: true,
                        backgroundColor: 'rgba(0, 0, 0, .8)'
                    },
                    xAxis: [{
                        type: 'category',
                        data: response.dates,
                        axisTick: {
                            alignWithLabel: true
                        },
                        splitLine: {
                            show: false
                        },
                        axisLine: {
                            show: true
                        }
                    }],
                    yAxis: [{
                        type: 'value',
                        axisLabel: {
                            formatter: '{value}'
                        },
                        min: 0,
                        max: 1000,
                        interval: 100,
                        axisLine: {
                            show: false
                        },
                        splitLine: {
                            show: true,
                            interval: 'auto'
                        }
                    }],
                    series: [{
                        name: 'Реализованные',
                        data: response.implementations,
                        label: {
                            show: false,
                            color: '#639'
                        },
                        type: 'bar',
                        barGap: 0,
                        color: '#62D893',
                        smooth: true,
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowOffsetY: -2,
                                shadowColor: 'rgba(0, 0, 0, 0.3)'
                            }
                        }
                    }, {
                        name: 'Оприходованные',
                        data: response.postings,
                        label: {
                            show: false,
                            color: '#639'
                        },
                        type: 'bar',
                        color: '#A9E4FF',
                        smooth: true,
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowOffsetY: -2,
                                shadowColor: 'rgba(0, 0, 0, 0.3)'
                            }
                        }
                    },
                    {
                        name: 'Списанные',
                        data: response.writeOffs,
                        label: {
                            show: false,
                            color: '#639'
                        },
                        type: 'bar',
                        color: '#FFC2D4',
                        smooth: true,
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowOffsetY: -2,
                                shadowColor: 'rgba(0, 0, 0, 0.3)'
                            }
                        }
                        },
                        {
                            name: 'Перемещенные',
                            data: response.transfers,
                            label: {
                                show: false,
                                color: '#639'
                            },
                            type: 'bar',
                            color: '#fb7185',
                            smooth: true,
                            itemStyle: {
                                emphasis: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowOffsetY: -2,
                                    shadowColor: 'rgba(0, 0, 0, 0.3)'
                                }
                            }
                        }]
                });
                $(window).on('resize', function () {
                    setTimeout(function () {
                        echartBar.resize();
                    }, 500);
                });
            }
        });
    }

    // Chart in Dashboard version 1

    var echartElemPie = document.getElementById('echartPieFacilityTop');

    if (echartElemPie) {
        $.ajax({
            type: 'GET',
            url: 'Ettn/Home/GetAllTopFacilitiesByProducts',
            dataType: 'json',
            success: function (response) {
                var echartPie = echarts.init(echartElemPie);
                echartPie.setOption({
                    color: ['#44546A', '#ED7D31', '#A5A5A5', '#FFC000', '#4472C4'],
                    tooltip: {
                        show: true,
                        backgroundColor: 'rgba(0, 0, 0, .8)'
                    },
                    series: [{
                        name: 'Топ 5 складов по товарам',
                        type: 'pie',
                        radius: '60%',
                        center: ['50%', '50%'],
                        data: response,
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }]
                });
                $(window).on('resize', function () {
                    setTimeout(function () {
                        echartPie.resize();
                    }, 500);
                });
            }
        })
    }

    // Chart in Dashboard version 1

    //var echartElem1 = document.getElementById('echart1');

    //if (echartElem1) {
    //    var echart1 = echarts.init(echartElem1);
    //    echart1.setOption(_objectSpread({}, echartOptions.lineFullWidth, {}, {
    //        series: [_objectSpread({
    //            data: [30, 40, 20, 50, 40, 80, 90]
    //        }, echartOptions.smoothLine, {
    //            markArea: {
    //                label: {
    //                    show: true
    //                }
    //            },
    //            areaStyle: {
    //                color: 'rgba(102, 51, 153, .2)',
    //                origin: 'start'
    //            },
    //            lineStyle: {
    //                color: '#663399'
    //            },
    //            itemStyle: {
    //                color: '#663399'
    //            }
    //        })]
    //    }));
    //    $(window).on('resize', function () {
    //        setTimeout(function () {
    //            echart1.resize();
    //        }, 500);
    //    });
    //} // Chart in Dashboard version 1

    //var echartElem2 = document.getElementById('echart2');

    //if (echartElem2) {
    //    var echart2 = echarts.init(echartElem2);
    //    echart2.setOption(_objectSpread({}, echartOptions.lineFullWidth, {}, {
    //        series: [_objectSpread({
    //            data: [30, 10, 40, 10, 40, 20, 90]
    //        }, echartOptions.smoothLine, {
    //            markArea: {
    //                label: {
    //                    show: true
    //                }
    //            },
    //            areaStyle: {
    //                color: 'rgba(255, 193, 7, 0.2)',
    //                origin: 'start'
    //            },
    //            lineStyle: {
    //                color: '#FFC107'
    //            },
    //            itemStyle: {
    //                color: '#FFC107'
    //            }
    //        })]
    //    }));
    //    $(window).on('resize', function () {
    //        setTimeout(function () {
    //            echart2.resize();
    //        }, 500);
    //    });
    //} // Chart in Dashboard version 1

    //var echartElem3 = document.getElementById('echart3');

    //if (echartElem3) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/Home/GetTwentyDaysSignalsCount',
    //        dataType: 'json',
    //        success: function (response) {
    //            var echart3 = echarts.init(echartElem3);
    //            console.log(JSON.stringify(response))
    //            echart3.setOption(_objectSpread({}, echartOptions.lineNoAxis, {}, {
    //                xAxis: {
    //                    data: response.date
    //                },
    //                yAxis: {},
    //                series: [{
    //                    name: 'Сигналы',
    //                    type: 'line',
    //                    smooth: true,
    //                    data: response.count
    //                }],
    //                lineStyle: _objectSpread({
    //                    color: 'rgba(102, 51, 153, 0.8)',
    //                    width: 3
    //                }, echartOptions.lineShadow),
    //                label: {
    //                    show: true,
    //                    color: '#212121'
    //                },
    //                itemStyle: {
    //                    borderColor: 'rgba(102, 51, 153, 1)'
    //                }
    //            }));
    //            $(window).on('resize', function () {
    //                setTimeout(function () {
    //                    echart3.resize();
    //                }, 500);
    //            });
    //        }
    //    })
    //}
});