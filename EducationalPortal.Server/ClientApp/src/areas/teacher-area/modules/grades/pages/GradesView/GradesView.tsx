import React, {useState} from 'react';
import {useQuery} from '@apollo/client';
import {Navigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {GET_GRADE_WITH_STUDENTS_QUERY, GetGradeWithStudentsData, GetGradeWithStudentsVars} from '../../grades.queries';
import {Space, Table} from 'antd';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {User} from '../../../users/users.types';
import Title from 'antd/es/typography/Title';

export const GradesView = () => {
    const params = useParams();
    const id = params.id as string;
    const [studentsPage, setStudentsPage] = useState(1);

    const getGradeQuery = useQuery<GetGradeWithStudentsData, GetGradeWithStudentsVars>(GET_GRADE_WITH_STUDENTS_QUERY,
        {variables: {id: id, studentsPage: studentsPage}},
    );

    const columns: ColumnsType<User> = [
        {
            title: 'Учень',
            dataIndex: 'student',
            key: 'student',
            render: (text, user) => (<>{user.firstName} {user.lastName}</>),
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text, user) => (
                <ButtonsVUR viewUrl={`../../students/${user.id}`} updateUrl={`../../students/update/${user.id}`}/>
            ),
        },
    ];

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getGradeQuery.loading)
        return <Loading/>;

    const grade = getGradeQuery.data?.getGrade;
    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title>{grade?.name}</Title>
            <table className="infoTable">
                <tbody>
                <tr>
                    <td>Класний керівник:</td>
                    <td>
                        <span>...</span>
                    </td>
                </tr>
                </tbody>
            </table>
            <Table
                rowKey={'id'}
                dataSource={grade?.students.entities}
                columns={columns}
                pagination={{
                    total: grade?.students.total,
                    onChange: async (newPage: number) => {
                        setStudentsPage(newPage);
                        await getGradeQuery.refetch({id: id, studentsPage: studentsPage});
                    },
                }}
            />
        </Space>
    );
};
