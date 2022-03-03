import React from 'react';
import {useQuery} from '@apollo/client';
import {Navigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {GET_SUBJECT_QUERY, GetSubjectData, GetSubjectVars} from '../../subjects.queries';

export const SubjectsView = () => {
    const params = useParams();
    const id = params.id as string;

    const getSubjectQuery = useQuery<GetSubjectData, GetSubjectVars>(GET_SUBJECT_QUERY,
        {variables: {id: id}},
    );

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getSubjectQuery.loading)
        return <Loading/>;

    const subject = getSubjectQuery.data?.getSubject;
    return (
        <div>
            <table className="infoTable">
                <tbody>
                <tr>
                    <td>Назва:</td>
                    <td>
                        <span>{subject?.name}</span>
                    </td>
                </tr>
                <tr>
                    <td>Викладач:</td>
                    <td>
                        <span>{subject?.teacher.firstName} {subject?.teacher.middleName} {subject?.teacher.lastName}</span>
                    </td>
                </tr>
                <tr>
                    <td>Навчальний рік:</td>
                    <td>
                        <span>{subject?.educationalYear.name}</span>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    );
};
