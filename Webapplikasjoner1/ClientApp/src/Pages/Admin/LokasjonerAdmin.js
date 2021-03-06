import React, {useCallback, useEffect, useState} from "react";
//Display lokasjoner
import {Button, Col, Row, Table} from "reactstrap";
import {BackButton} from "../../components/Admin/BackButton";
import {LeggTilLokasjon} from "../../components/Admin/LeggTil/LeggTilLokasjon";
import axios from "axios";
import qs from "qs";
import {checkUnauthorized} from "../../utils/checkUnauthorized";


export const Lokasjoner = ({ apiData, refetch }) => {
    const deleteLokasjon = (id) => {
        axios.post('/Lokasjon/Slett', qs.stringify({ id }))
            .then(() => {
                refetch()
            })
            .catch(e => {
                console.log(e)
                checkUnauthorized(e)
            })
    }
    return (
        <div>
            <Row className={'p-3'}>
                <BackButton/>
            </Row>
            <Row>
                <Col
                    md={6}
                    sm={12}
                >
                    <Table className={'table border'}>
                        <thead className={'thead-light'}>
                        <tr>
                            <td><h4>Lokasjoner</h4></td>
                        </tr>
                        </thead>
                        <tbody>
                        {apiData?.lokasjoner?.map(lokasjon => (
                            <tr
                                key={lokasjon.stedsNummer}
                            >
                                <td>
                                    {lokasjon.stedsnavn}
                                </td>
                                <td>
                                    <Button
                                        className={'btn btn-danger'}
                                        onClick={() => deleteLokasjon(lokasjon.stedsNummer)}
                                    >
                                        Slett
                                    </Button>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            <Row className={'p-3'}>
                <LeggTilLokasjon
                    refetch={refetch}
                />
            </Row>
        </div>
    )
};
//Legge til
//Slette



