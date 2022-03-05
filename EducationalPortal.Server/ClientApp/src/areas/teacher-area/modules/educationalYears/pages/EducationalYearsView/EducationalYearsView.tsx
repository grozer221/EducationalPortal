import React from 'react';
import {useQuery} from '@apollo/client';
import {
    GET_EDUCATIONAL_YEAR_QUERY,
    GetEducationalYearData,
    GetEducationalYearVars,
} from '../../educationalYears.queries';
import {Navigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {Space, Tag} from 'antd';
import Title from 'antd/es/typography/Title';

export const EducationalYearsView = () => {
    const params = useParams();
    const id = params.id as string;

    const getEducationalYearQuery = useQuery<GetEducationalYearData, GetEducationalYearVars>(GET_EDUCATIONAL_YEAR_QUERY,
        {variables: {id: id}},
    );

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getEducationalYearQuery.loading)
        return <Loading/>;

    const educationalYear = getEducationalYearQuery.data?.getEducationalYear;
    return (
        <Space direction={'vertical'} size={20}>
            <Title>{educationalYear?.name}</Title>
            <table className="infoTable">
                <tbody>
                <tr>
                    <td>Поточний:</td>
                    <td>
                        {educationalYear?.isCurrent
                            ? <Tag color="green">Так</Tag>
                            : <Tag color="red">Ні</Tag>
                        }
                    </td>
                </tr>
                <tr>
                    <td>Дата початку:</td>
                    <td>
                        <span>{educationalYear?.dateStart}</span>
                    </td>
                </tr>
                <tr>
                    <td>Дата кінця:</td>
                    <td>
                        <span>{educationalYear?.dateEnd}</span>
                    </td>
                </tr>
                </tbody>
            </table>
        </Space>
    );
};
