import React, {useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {message, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link} from 'react-router-dom';
import {GET_USERS_WITH_GRADE_QUERY, GetUsersWithGradeData, GetUsersWithGradeVars} from '../../users.queries';
import {Role, User} from '../../users.types';
import {REMOVE_USER_MUTATION, RemoveUserData, RemoveUserVars} from '../../users.mutations';
import Title from 'antd/es/typography/Title';
import {isAdministrator} from '../../../../../../utils/permissions';

export const StudentsIndex = () => {
    const [page, setPage] = useState(1);
    const [roles, setRoles] = useState([Role.Student]);
    const getStudentsQuery = useQuery<GetUsersWithGradeData, GetUsersWithGradeVars>(GET_USERS_WITH_GRADE_QUERY,
        {variables: {page: page, roles: roles}, fetchPolicy: 'network-only'},
    );
    const [removeStudentMutation, removeStudentMutationOptions] = useMutation<RemoveUserData, RemoveUserVars>(REMOVE_USER_MUTATION);

    const onRemove = (studentId: string) => {
        removeStudentMutation({variables: {id: studentId}})
            .then(async (response) => {
                await getStudentsQuery.refetch({page, roles: roles});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<User> = [
        {
            title: 'Учень',
            dataIndex: 'student',
            key: 'student',
            render: (text, student) => <>{student?.lastName} {student?.firstName}</>,
        },
        {
            title: 'Клас',
            dataIndex: 'grade',
            key: 'grade',
            render: (text, student) => student?.grade
                ? <Link to={`../../grades/${student.gradeId}`}>{student.grade?.name}</Link>
                : '--',
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text: string, student) => (
                // isAdministrator()
                //     ? <ButtonsVUR viewUrl={`${student?.id}`} updateUrl={`update/${student?.id}`}
                //                   onRemove={() => onRemove(student?.id)}/>
                //     : <ButtonsVUR viewUrl={`${student?.id}`}/>
                <ButtonsVUR viewUrl={`${student?.id}`} updateUrl={`update/${student?.id}`}
                            onRemove={() => onRemove(student?.id)}/>
            ),
        },
    ];

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Учні</Title>
            {/*{isAdministrator() &&*/}
            {/*<Link to={'create'}>*/}
            {/*    <ButtonCreate/>*/}
            {/*</Link>*/}
            {/*}*/}
            <Link to={'create'}>
                <ButtonCreate/>
            </Link>
            <Table
                style={{width: '100%'}}
                rowKey={'id'}
                loading={getStudentsQuery.loading || removeStudentMutationOptions.loading}
                dataSource={getStudentsQuery.data?.getUsers.entities}
                columns={columns}
                pagination={{
                    total: getStudentsQuery.data?.getUsers.total,
                    onChange: async (newPage: number) => {
                        setPage(newPage);
                        await getStudentsQuery.refetch({page: newPage, roles: roles});
                    },
                }}
            />
        </Space>
    );
};
